using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using UserProductAPI.Core.Entities;

namespace UserProductAPI.Infrastructure.Data
{
    public static class SeedData
    {
        public static void EnsureDefaultUserExists(IServiceProvider serviceProvider)
        {
            using (var context = new UserProductAuthDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<UserProductAuthDbContext>>()))
            {
                if (!context.Users.Any(u => u.Id == "default-user-id"))
                {
                    context.Users.Add(new User
                    {
                        Id = "default-user-id",
                        Address = "default address",
                        Age = 0,
                        City = "default city",
                        FirstName = "default first name",
                        LastName = "default last name",
                        Email = "default@example.com",
                        UserName = "defaultusername",
                        NormalizedUserName = "DEFAULTUSERNAME",
                        NormalizedEmail = "DEFAULT@EXAMPLE.COM",
                        EmailConfirmed = false,
                        PasswordHash = "default password hash",
                        SecurityStamp = "default security stamp",
                        ConcurrencyStamp = "default concurrency stamp",
                        PhoneNumber = "default phone",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = false,
                        AccessFailedCount = 0
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

