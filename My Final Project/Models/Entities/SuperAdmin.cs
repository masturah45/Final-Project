using System.ComponentModel.DataAnnotations.Schema;

namespace My_Final_Project.Models.Entities
{
    public class SuperAdmin : BaseEntity
    {
        public User User { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
    }
}
