using Microsoft.EntityFrameworkCore;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.ApplicationContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Issue>  Issues { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TherapistIssue> TherapistIssues { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
