namespace My_Final_Project.Models.Entities
{
    public class SuperAdmin : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
