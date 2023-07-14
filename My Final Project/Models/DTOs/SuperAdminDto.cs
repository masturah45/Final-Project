namespace My_Final_Project.Models.DTOs
{
    public class SuperAdminDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }

    public class CreateSuperAdminRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }

    public class UpdateSuperAdminRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }
}

