using Microsoft.EntityFrameworkCore;
using MonstarHacks.Fugees.Backend.Models;

namespace MonstarHacks.Fugees.Backend
{
    public class FugeesDbContext : DbContext
    {

        public FugeesDbContext() { }

        public FugeesDbContext(DbContextOptions<FugeesDbContext> options) : base(options)
        {
            
        }

        public DbSet<HealthcareProfessional> HealthcareProfessionals { get; set; }
        public DbSet<HealthcareProfessionalSpecialtyType> HealthcareProfessionalSpecialtyTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MedicalSupply> MedicalSupplies { get; set; }
        public DbSet<MedicalSupplyDonation> MedicalSupplyDonations { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("FugeesDb");
            optionsBuilder.UseSqlServer(connectionString, x=>x.UseNetTopologySuite());
        }

    }
}
