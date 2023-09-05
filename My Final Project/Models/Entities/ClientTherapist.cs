namespace My_Final_Project.Models.Entities
{
    public class ClientTherapist : BaseEntity
    {
        public Guid TherapistId { get; set; }
        public Guid ClientId { get; set; }
        public Therapist Therapist { get; set; }
        public Client Client { get; set; }
    }
}
