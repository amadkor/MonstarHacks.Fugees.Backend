using MonstarHacks.Fugees.Backend.DTOs;
using NetTopologySuite.Geometries;

namespace MonstarHacks.Fugees.Backend.Models
{
    public class HealthcareProfessional
    {
        public int Id { get; set; }
        public User User { get; set; }
        public HealthcareProfessionalSpecialtyType Speciality { get; set; }
        public bool isVerified { get; set; }

        public string? CertificateURI { get; set; }


        public virtual HealthcareProfessionalDTO toDTO()
        {
            return new HealthcareProfessionalDTO()
            {
                Id = Id,
                User = User.toDTO(),
                Speciality = Speciality,
                isVerified = isVerified
            };
        }
    }

    public class HealthcareProfessionalSpecialtyType
    {
        public int Id { get;set; }
        public string SpecialityName { get; set; }
    }


}
