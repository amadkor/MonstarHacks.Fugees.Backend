using NetTopologySuite.Geometries;

namespace MonstarHacks.Fugees.Backend.Models
{
    public class Seeder
    {
        private readonly FugeesDbContext fugeesDContext;
        public Seeder(FugeesDbContext fugeesDContext)
        {
            this.fugeesDContext = fugeesDContext;
        }

        public async void Seed()
        {
            
            if (!fugeesDContext.HealthcareProfessionals.Any())
            {

                var Dental = new HealthcareProfessionalSpecialtyTypes()
                {
                    SpecialityName = "Dental"
                };
                var Emergency = new HealthcareProfessionalSpecialtyTypes()
                {
                    SpecialityName = "Emergency"
                };


                var HCPTypes = new List<HealthcareProfessionalSpecialtyTypes>()
                {
                    Dental,
                    Emergency
                };
                fugeesDContext.HealthcareProfessionalSpecialtyTypes.AddRange(HCPTypes);

                fugeesDContext.SaveChanges();


                var HCPs = new List<HealthcareProfessional>() {
                    new HealthcareProfessional(){
                        Name ="Dental HCP",
                        Speciality = Dental,
                        LastKnownLocation =new Point(13.003725d, 55.604870d) { SRID = 4326 }
                    },
                    new HealthcareProfessional(){
                        Name = "Emergency HCP",
                        Speciality = Emergency,
                        LastKnownLocation =new Point(23.003725d, 55.604870d) { SRID = 4326 } 

                    },
                    new HealthcareProfessional(){
                        Name ="Dental HCP1",
                        Speciality = Dental,
                        LastKnownLocation =new Point(33.003725d, 55.604870d) { SRID = 4326 }
                    },
                    new HealthcareProfessional(){
                        Name = "Emergency HCP1",
                        Speciality = Emergency,
                        LastKnownLocation =new Point(43.003725d, 55.604870d) { SRID = 4326 }

                    }
                };
                fugeesDContext.HealthcareProfessionals.AddRange(HCPs);
                fugeesDContext.SaveChanges();
            }


        }
    }
}

