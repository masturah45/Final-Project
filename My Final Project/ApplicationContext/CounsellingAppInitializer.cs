using Microsoft.EntityFrameworkCore;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.ApplicationContext;

public class CounsellingAppInitializer
{
    public static async Task Seed (IApplicationBuilder applicationBuilder)
    {
        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = "SuperAdmin",
            Description = "SuperAdmin",
            DateCreated = DateTime.Now,
        };

       // var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Adesanya",
            LastName = "Masturah",
            Password = "dolapo345",
            Email = "masturahadesanya@gmail.com",
            PhoneNumber = "09051643452",
            IsDeleted = false,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
            Gender = Models.Enum.Gender.Female,
            //SuperAdmin = superAdmin,
            UserName = "superAdmin",
            //UserRoles = userRole,
        };

        var userRole = new List<UserRole>
        {
            new UserRole()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = role.Id,
                DateCreated = DateTime.Now,
                Role = role,
                User = user,
                
            },
        };
        user.UserRoles = userRole;
        var superAdmin = new SuperAdmin()
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
            UserId = user.Id,
            AccountNumber = "0159192507",
            BankName = "GTBank",
            User = user,
        };

       user.SuperAdmin = superAdmin;

        using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
            if(!context.Roles.Any())
            {
                context.Users.AddAsync(user);
                context.SaveChangesAsync();
            }
        }

    }
}
