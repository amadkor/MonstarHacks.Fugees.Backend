using Microsoft.EntityFrameworkCore;
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
            
            fugeesDContext.Database.Migrate();

            var seed = true;

            if (seed && !fugeesDContext.HealthcareProfessionals.Any())
            {

                var Dental = new HealthcareProfessionalSpecialtyType()
                {
                    SpecialityName = "Dental"
                };
                var Emergency = new HealthcareProfessionalSpecialtyType()
                {
                    SpecialityName = "Emergency"
                };


                var HCPTypes = new List<HealthcareProfessionalSpecialtyType>()
                {
                    Dental,
                    Emergency
                };
                fugeesDContext.HealthcareProfessionalSpecialtyTypes.AddRange(HCPTypes);

                fugeesDContext.SaveChanges();

                var User1 = new User()
                {
                    Name = "HCP 1",
                    IsMedicalProfessional = true,
                    PhoneNumber = "971567045146",
                    LastKnownLocation = new Point(55.141736, 25.071844) { SRID = 4326 }

                };
                var User2 = new User()
                {
                    Name = "HCP 2",
                    IsMedicalProfessional = true,
                    PhoneNumber = "971567045146"
                };
                var User3 = new User()
                {
                    Name = "User 1",
                    IsMedicalProfessional = false,
                    PhoneNumber = "971567045146"
                };

                var Users = new List<User>() {
                    User1, User2, User3
                };
                fugeesDContext.Users.AddRange(Users);
                fugeesDContext.SaveChanges();



                var HCPs = new List<HealthcareProfessional>() {
                    new HealthcareProfessional(){
                        Speciality = Dental,
                        User = User1,
                        isVerified = true
                    },
                    new HealthcareProfessional(){
                        Speciality = Emergency,
                        User = User2,
                        isVerified = false
                    },
                //    new HealthcareProfessional(){
                //        Name = "Emergency HCP",
                //        Speciality = Emergency,
                //        LastKnownLocation =new Point(23.003725d, 55.604870d) { SRID = 4326 } 

                //    },
                //    new HealthcareProfessional(){
                //        Name ="Dental HCP1",
                //        Speciality = Dental,
                //        LastKnownLocation =new Point(33.003725d, 55.604870d) { SRID = 4326 }
                //    },
                //    new HealthcareProfessional(){
                //        Name = "Emergency HCP1",
                //        Speciality = Emergency,
                //        LastKnownLocation =new Point(43.003725d, 55.604870d) { SRID = 4326 }

                //    }
                };
                fugeesDContext.HealthcareProfessionals.AddRange(HCPs);
                fugeesDContext.SaveChanges();

                var MedicalSupply1 = new MedicalSupply();
            }


        }
    }
}

