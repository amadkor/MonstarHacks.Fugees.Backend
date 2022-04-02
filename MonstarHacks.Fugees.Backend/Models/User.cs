using MonstarHacks.Fugees.Backend.DTOs;
using NetTopologySuite.Geometries;

namespace MonstarHacks.Fugees.Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? IdentityProviderId { get; set; }
        public string? Name { get; set; }
        public bool IsMedicalProfessional { get; set; }
        public Point? LastKnownLocation { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual UserDTO toDTO()
        {
            return new UserDTO()
            {
                Id = Id,
                IdentityProviderId = IdentityProviderId,
                Name = Name,   
                PhoneNumber = PhoneNumber,  
                IsMedicalProfessional = IsMedicalProfessional,
                latitude = LastKnownLocation != null ? LastKnownLocation.Y : 0,
                longitude = LastKnownLocation != null ? LastKnownLocation.X : 0
            };
        }
    }
}
