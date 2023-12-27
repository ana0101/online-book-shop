using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.ContextModels;

namespace OnlineBookShop.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ShopContext(serviceProvider.GetRequiredService<DbContextOptions<ShopContext>>()))
            {
                // verific daca exista deja roluri pentru a nu le insera de mai multe ori
                if (context.Roles.Any())
                {
                    return;
                }

                context.Roles.AddRange(
                    new IdentityRole
                    { 
                        Id = "25736e23-afe5-48b2-b5e7-9fe2fe194c75", 
                        Name = "Admin", 
                        NormalizedName = "Admin".ToUpper() 
                    },

                    new IdentityRole 
                    { 
                        Id = "ee659f59-cd44-4145-9d59-35e76aa81abf", 
                        Name = "User", 
                        NormalizedName = "User".ToUpper() 
                    }
                );

                var passwordHasher = new PasswordHasher<ApplicationUser>();

                context.Users.AddRange(
                    new ApplicationUser
                    {
                        Id = "cee7e6e6-ba45-40af-89f2-ecbbd9b87c8e",
                        UserName = "adminTest",
                        NormalizedUserName = "adminTest".ToUpper(),
                        Email = "admin@admin.com",
                        NormalizedEmail = "admin@admin.com".ToUpper(),
                        EmailConfirmed = true,
                        PasswordHash = passwordHasher.HashPassword(null, "admin")
                    },

                    new ApplicationUser
                    {
                        Id = "a7f7a5eb-4efe-4a93-b89e-edf82ddbdd85",
                        UserName = "userTest",
                        NormalizedUserName = "userTest".ToUpper(),
                        Email = "user@user.com",
                        NormalizedEmail = "user@user.com".ToUpper(),
                        EmailConfirmed = true,
                        PasswordHash = passwordHasher.HashPassword(null, "user")
                    }
                );

                context.UserRoles.AddRange(
                    new IdentityUserRole<string>
                    {
                        UserId = "cee7e6e6-ba45-40af-89f2-ecbbd9b87c8e",
                        RoleId = "25736e23-afe5-48b2-b5e7-9fe2fe194c75"
                    },

                    new IdentityUserRole<string>
                    {
                        UserId = "a7f7a5eb-4efe-4a93-b89e-edf82ddbdd85",
                        RoleId = "ee659f59-cd44-4145-9d59-35e76aa81abf"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
