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
            var user = await userManager.FindByEmailAsync("farmer@example.com");
            if (user == null)
            {
                user = new User
                {
                    UserName = "farmer@example.com",
                    Email = "farmer@example.com"
                };

                var createUserResult = await userManager.CreateAsync(user, "Password123!");
                if (createUserResult.Succeeded)
                {
                    // Assign the "Farmer" role to this user
                    await userManager.AddToRoleAsync(user, "Farmer");
                }
            }
        }
    }
}
