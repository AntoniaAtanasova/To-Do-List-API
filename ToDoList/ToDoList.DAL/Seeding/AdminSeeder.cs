using Microsoft.AspNetCore.Identity;
using System;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Seeding
{
    public class AdminSeeder : ISeeder
    {
        public async System.Threading.Tasks.Task SeedAsync(DatabaseContext context, IServiceProvider serviceProvider)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User admin = new User()
            {
                Id = Guid.NewGuid().ToString("D"),
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@test.test",
                NormalizedEmail = "admin@test.test".ToUpper(),
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            admin.PasswordHash = hasher.HashPassword(admin, "adminpass");

            IdentityRole adminRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString("D"),
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };

            IdentityRole userRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString("D"),
                Name = "User",
                NormalizedName = "User".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")
            };


            IdentityUserRole<string> adminUserRole = new IdentityUserRole<string>()
            {
                RoleId = adminRole.Id,
                UserId = admin.Id
            };

            await context.Roles.AddAsync(adminRole);
            await context.Roles.AddAsync(userRole);
            await context.Users.AddAsync(admin);
            await context.UserRoles.AddAsync(adminUserRole);
        }
    }
}
