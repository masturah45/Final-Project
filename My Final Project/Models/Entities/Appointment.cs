using My_Final_Project.Models.Enum;

namespace My_Final_Project.Models.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid ClientId { get; set; }
        public Guid TherapistId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Duration { get; set; }
        public Day Day { get; set; }
        public string MeetingLink { get; set; }
    }
}
