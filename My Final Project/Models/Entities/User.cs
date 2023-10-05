using My_Final_Project.Models.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace My_Final_Project.Models.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName Name is required.")]
        public string LastName { get; set; }
        [Required]
        [Range(1,16, ErrorMessage ="PhoneNumber must not be more than 1-16")]
        public string PhoneNumber { get; set; }
        public SuperAdmin SuperAdmin { get; set; }
        public Therapist Therapist { get; set; }        
        public Client Client { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        public IList<UserRole> UserRoles { get; set; }
    }
}
