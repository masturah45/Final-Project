namespace My_Final_Project.Models.Entities
{
    public class Booking : BaseEntity
    {
        public Guid TherapistId { get; set; }
        public Guid ClientId { get; set; }
        public Therapist Therapist { get; set; }
        public Client Client { get; set; }
        public DateTime AppointmentDateTime { get; set; } 
        public bool IsCancelled { get; set; } = false;
    }
}
