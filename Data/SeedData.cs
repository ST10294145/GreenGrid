using Microsoft.AspNetCore.Identity;
using GreenGrid.Models;

namespace GreenGrid.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create roles if they don't exist
            string[] roleNames = { "Farmer", "Employee" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create a default admin user (Farmer)
            var adminEmail = "farmer.admin@greengrid.com";
            var adminPassword = "Farmer@123";

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new User
                {
                    Name = "Admin Farmer",              
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Province = "Gauteng",
                    Department = "Crop Production"
                };

                var createUserResult = await userManager.CreateAsync(user, adminPassword);
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Farmer");
                }
            }
        }
    }
}
