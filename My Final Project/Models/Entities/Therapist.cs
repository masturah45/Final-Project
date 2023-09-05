using My_Final_Project.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace My_Final_Project.Models.Entities
{
    public class Therapist : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }
        public string Certificate { get; set; }
        public string Credential { get; set; }
        [Required(ErrorMessage = "RegNo is required.")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public string ProfilePicture { get; set; }
        public IList<Booking> Bookings { get; set; }
        public Aprrove Status { get; set; } = Aprrove.Pending;
        public IList<TherapistIssue> TherapistIssues { get; set; }
        public bool IsAvalaible { get; set; } = false;
        public IList<Client> Clients { get; set; }
    }
}
