using My_Final_Project.Models.Enum;

namespace My_Final_Project.Models.DTOs
{
    public class TherapistDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
        public Gender Gender { get; set; }
    }

    public class CreateTherapistRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Gender Gender { get; set; }
    }

    public class UpdateTherapistRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
    }
}

