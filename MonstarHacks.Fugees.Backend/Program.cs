using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonstarHacks.Fugees.Backend;
using MonstarHacks.Fugees.Backend.DTOs;
using MonstarHacks.Fugees.Backend.Models;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using MonstarHacks.Fugees.Backend.Helpers;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("FugeesDb");

builder.Services.AddDbContext<FugeesDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<Seeder>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
     {
         c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
         c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
         {
             ValidAudience = builder.Configuration["Auth0:Audience"],
             ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}"
         };
     });

builder.Services.AddAuthorization();

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


//builder.Services.AddSwaggerGen(c =>
//{
//    var securityScheme = new OpenApiSecurityScheme
//    {
//        Name = "JWT Authentication",
//        Description = "Enter JWT Bearer token * *_only_ * *",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer", // must be lower case
//        BearerFormat = "JWT",
//        Reference = new OpenApiReference
//        {
//            Id = JwtBearerDefaults.AuthenticationScheme,
//            Type = ReferenceType.SecurityScheme
//        }
//    };
//    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {securityScheme, new string[] { }}
//    });
//});




var app = builder.Build();

SeedData(app);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseAuthentication();
app.UseAuthorization();


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
    return fugeesDbContext.HealthcareProfessionals.Include(HCP=>HCP.Speciality).Include(HCP => HCP.User).Select(x=>x.toDTO());
});

app.MapGet("/HCP/{specialty}", (int? specialtyId, int? distance, double? latitude, double? longitude, [FromServices] FugeesDbContext fugeesDbContext) =>
{

    var result = fugeesDbContext.HealthcareProfessionals.AsQueryable();
    if (specialtyId != null)
    {
        result = result.Where(hcp => hcp.Speciality.Id == specialtyId);
    }
    if (distance != null && latitude != null && longitude != null)
    {
        var currentLocation = new Point(longitude.Value, latitude.Value) { SRID = 4326 };
        result = result.Where(x => x.User.LastKnownLocation.Distance(currentLocation) <= distance);
    }
    return result.Include(HCP => HCP.Speciality).Include(HCP => HCP.User).Select(x => x.toDTO());
});

app.MapGet("/PostLogin", async (HttpContext context, [FromServices] FugeesDbContext fugeesDbContext) =>
{
    var IdentityUserId = IdentityHelpers.GetSubjectForContext(context);
    if (fugeesDbContext.Users.Count(u => u.IdentityProviderId == IdentityUserId) == 0)
    {
        var user = new User()
        {
            IdentityProviderId = IdentityUserId,
        };
        fugeesDbContext.Users.Add(user);
        await fugeesDbContext.SaveChangesAsync();
    }
}).RequireAuthorization();

app.MapPost("/RegisterHCP", async (HttpContext context, [FromServices] FugeesDbContext fugeesDbContext, HealthcareProfessionalDTO healthcareProfessional) => {
    var IdentityUserId = IdentityHelpers.GetSubjectForContext(context);
    var user = fugeesDbContext.Users.FirstOrDefault(u => u.IdentityProviderId == IdentityUserId);
    if (user!=null)
    {
        user.IsMedicalProfessional = true;
        var HCPProfile = new HealthcareProfessional()
        {
            Speciality = healthcareProfessional.Speciality,
            User = user
        };
        fugeesDbContext.HealthcareProfessionals.Add(HCPProfile);
        await fugeesDbContext.SaveChangesAsync();
    }
}).RequireAuthorization();

app.MapPost("/uploadHCPCertification",
    async Task<IResult> (HttpContext context, [FromServices] FugeesDbContext fugeesDbContext, HttpRequest request) =>
    {
        if (!request.HasFormContentType)
            return Results.BadRequest();

        var form = await request.ReadFormAsync();
        var formFile = form.Files["file"];

        if (formFile is null || formFile.Length == 0)
            return Results.BadRequest();


        var IdentityUserId = IdentityHelpers.GetSubjectForContext(context);
        var user = fugeesDbContext.Users.FirstOrDefault(u => u.IdentityProviderId == IdentityUserId);


        var blobConnectionString = builder.Configuration.GetValue<string>("BlobConnectionString");
        var blobContainerName = builder.Configuration.GetValue<string>("BlobContainerName");

        var blobServiceClient = new BlobServiceClient(blobConnectionString);

        var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);

        var blobClient = blobContainerClient.GetBlobClient(user.Id+" - HCPCertification.jpg");

        await using var stream = formFile.OpenReadStream();

        var HCPProfile = fugeesDbContext.HealthcareProfessionals.FirstOrDefault(hcp => hcp.User.Id == user.Id);
        if (HCPProfile is null)
        {
            return Results.BadRequest();
        }
        HCPProfile.CertificateURI = blobClient.Uri.ToString();
        var result = await blobClient.UploadAsync(stream,true);
        await fugeesDbContext.SaveChangesAsync();

        return Results.Ok();
    }).Accepts<IFormFile>("multipart/form-data").RequireAuthorization();
;


//app.MapPost("/HCP", (HealthcareProfessionalDTO healthcareProfessional, HttpRequest request) =>
//{
//    return;
//});

//app.MapPost("/UploadHCPFile/{id}", (int id, IFormFile file) =>
//{
//    return;
//})
//    .Accepts<IFormFile>("multipart/form-data");



#endregion

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
