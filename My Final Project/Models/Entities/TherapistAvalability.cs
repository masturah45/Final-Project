using My_Final_Project.Models.Enum;

namespace My_Final_Project.Models.Entities
{
    public class TherapistAvalability
    {
        public Guid Id { get; set; }
        public Guid TherapistId { get; set; }
        public Day Day { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
