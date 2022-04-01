using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonstarHacks.Fugees.Backend;
using MonstarHacks.Fugees.Backend.Models;
using NetTopologySuite.Geometries;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FugeesDb");

builder.Services.AddDbContext<FugeesDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<Seeder>();


void SeedData(IHost app)
{
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seeder>();
        service.Seed();
    }
}
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


#region HCPs

app.MapGet("/HCP", ([FromServices] FugeesDbContext fugeesDbContext) =>
{
    return fugeesDbContext.HealthcareProfessionals.Select(x=>x.toDTO());
});

app.MapGet("HCP/{specialty}", (int? specialtyId, int? distance, double? latitude, double? longitude, [FromServices] FugeesDbContext fugeesDbContext) =>
{

    var result = fugeesDbContext.HealthcareProfessionals.AsQueryable();
    if (specialtyId != null)
    {
        result = result.Where(hcp => hcp.Speciality.Id == specialtyId);
    }
    if (distance != null && latitude != null && longitude != null) { 
        var currentLocation = new Point(longitude.Value, latitude.Value) { SRID = 4326 };
        result = result.Where(x => x.LastKnownLocation.Distance(currentLocation) <= distance);
    }
    return result.Include(hcp=>hcp.Speciality).Select(x=>x.toDTO());
});

//app.MapPost("HCP", HealthcareProfessional healthcareProfessional, [FromForm] FileModel)



#endregion

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}