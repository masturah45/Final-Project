using System.Data;

namespace My_Final_Project.Models.Entities
{
    public class UserRole : BaseEntity
    {
        public string UserId { get; set; }
        public Guid RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
