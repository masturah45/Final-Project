using My_Final_Project.Models.Enum;
using System.Reflection;

namespace My_Final_Project.Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public SuperAdmin SuperAdmin { get; set; }
        public Therapist Therapist { get; set; }
        public Client Client { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public IList<UserRole> UserRoles { get; set; }
    }
}
