using MonstarHacks.Fugees.Backend.Models;

namespace MonstarHacks.Fugees.Backend.DTOs
{
    public class HealthcareProfessionalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HealthcareProfessionalSpecialtyTypes Speciality { get; set; }
        public bool isVerified { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
