using MonstarHacks.Fugees.Backend.DTOs;
using NetTopologySuite.Geometries;

namespace MonstarHacks.Fugees.Backend.Models
{
    public class HealthcareProfessional
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HealthcareProfessionalSpecialtyTypes Speciality { get; set; }
        public bool isVerified { get; set; }

        public Point? LastKnownLocation { get; set; }

        public virtual HealthcareProfessionalDTO toDTO()
        {
            return new HealthcareProfessionalDTO() { 
                Id = Id, 
                Name = Name, 
                Speciality = Speciality,
                isVerified = isVerified,
                latitude = LastKnownLocation!=null?LastKnownLocation.Y:0,
                longitude = LastKnownLocation!=null?LastKnownLocation.X:0
            };
        }
    }

    public class HealthcareProfessionalSpecialtyTypes
    {
        public int Id { get;set; }
        public string SpecialityName { get; set; }
    }


}
