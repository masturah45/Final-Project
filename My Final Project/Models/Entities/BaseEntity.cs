using System.ComponentModel.DataAnnotations;

namespace My_Final_Project.Models.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
