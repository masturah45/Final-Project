﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using My_Final_Project.Models.Entities;

namespace My_Final_Project.ApplicationContext;

public static class CounsellingAppInitializer
{
    public static async Task Seed ( this IApplicationBuilder applicationBuilder)
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
            Id = Guid.NewGuid().ToString(),
            FirstName = "Adesanya",
            LastName = "Masturah",
            Email = "masturahadesanya@gmail.com",
            PhoneNumber = "09051643452",
            IsDeleted = false,
            DateCreated = DateTime.Now,
            DateUpdated = DateTime.Now,
            Gender = Models.Enum.Gender.Female,
            UserName = "masturahadesanya@gmail.com"
            
            //SuperAdmin = superAdmin,
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
            UserId =  Guid.Parse(user.Id).ToString(),
            User = user,
        };

       user.SuperAdmin = superAdmin;

        using(var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var manager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            await context.Database.MigrateAsync();
            if(!context.Roles.Any())
            {
                await context.Roles.AddAsync(role);
                await manager.CreateAsync(user, "@244341Ay");
                await context.UserRoles.AddRangeAsync(userRole);
                await context.SuperAdmins.AddRangeAsync(superAdmin);
                await context.SaveChangesAsync();
            }
        }

    }
}
