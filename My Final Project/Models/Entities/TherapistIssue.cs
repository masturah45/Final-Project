namespace My_Final_Project.Models.Entities
{
    public class TherapistIssue : BaseEntity
    {
        public Guid TherapistId { get; set; }
        public Guid IssueId { get; set; }
        public Therapist Therapist { get; set; }
        public Issue Issue { get; set; }
    }
}
