using MonstarHacks.Fugees.Backend.Models;

namespace MonstarHacks.Fugees.Backend.DTOs
{
    public class HealthcareProfessionalDTO
    {
        public int Id { get; set; }
        public UserDTO User{ get; set; }
        public HealthcareProfessionalSpecialtyType Speciality { get; set; }
        public bool isVerified { get; set; }

        
    }
}
