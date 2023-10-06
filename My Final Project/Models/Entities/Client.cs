using My_Final_Project.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace My_Final_Project.Models.Entities
{
    public class Client : BaseEntity
    {
        public User User { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string State { get; set; }
        public IList<Therapist> Therapists { get; set; }
        public IList<Booking> Bookings { get; set; }

    }
}
