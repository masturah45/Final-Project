namespace My_Final_Project.Models.Entities
{
    public class Therapist : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string BankName { get; set; }
        public double AmountByHour { get; set; }
        public string AccountNumber { get; set; }
        public IList<Appointment> Appointments { get; set; }
        public IList<Client> Clients { get; set; }
    }
}
