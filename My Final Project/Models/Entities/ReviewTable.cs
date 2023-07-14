namespace My_Final_Project.Models.Entities
{
    public class ReviewTable : BaseEntity
    {
        public Guid AppointmentId { get; set; }
        public string Ratings { get; set; }
        public string Review { get; set; }
    }
}
