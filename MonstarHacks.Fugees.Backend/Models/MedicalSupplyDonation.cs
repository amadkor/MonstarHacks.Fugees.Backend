namespace MonstarHacks.Fugees.Backend.Models
{
    public class MedicalSupplyDonation
    {
        public int Id { get; set; }
        //public User? User { get; set; }
        public MedicalSupply MedicalSupplies { get; set; }
        public int MedicalSuppliesId { get; set; }
        public int Quantity { get; set; }
        public bool isAvailable { get; set; }
    }
}
