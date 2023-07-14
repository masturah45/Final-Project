namespace My_Final_Project.Models.Entities
{
    public class SuperAdmin : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }
}
