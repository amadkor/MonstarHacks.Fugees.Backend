namespace MonstarHacks.Fugees.Backend.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? IdentityProviderId { get; set; }
        public string Name { get; set; }
        public bool IsMedicalProfessional { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
