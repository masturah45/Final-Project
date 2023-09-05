using My_Final_Project.Models.Enum;
using System.Reflection;

namespace My_Final_Project.Models.Entities
{
    public class Client : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string State { get; set; }
        public IList<Therapist> Therapists { get; set; }
        public IList<Booking> Bookings { get; set; }

    }
}
