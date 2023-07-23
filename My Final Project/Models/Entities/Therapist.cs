using My_Final_Project.Models.Enum;

namespace My_Final_Project.Models.Entities
{
    public class Therapist : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public Aprrove Status { get; set; } = Aprrove.Pending;
        public IList<Appointment> Appointments { get; set; }
        public IList<Client> Clients { get; set; }
    }
}
