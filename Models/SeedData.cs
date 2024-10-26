using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TennisFinalGrp339.Models;

namespace TennisFinalGrp339.Data
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            // Obtain UserManager and RoleManager from DI
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Check if the Admin role exists, and create it if not
            const string adminRole = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Check if an admin user already exists
            const string adminEmail = "admin@admin.com";
            if (await userManager.Users.AllAsync(u => u.Email != adminEmail))
            {
                // Create the Admin user
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin"
                };

                // Create the user with the specified password
                var result = await userManager.CreateAsync(adminUser, "Admin.123");

                if (result.Succeeded)
                {
                    // Assign the Admin role to the user
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
                else
                {
                    throw new Exception("Failed to create Admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
