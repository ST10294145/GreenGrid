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

            // Create a default Farmer user
            var farmerEmail = "farmer.admin@greengrid.com";
            var farmerPassword = "Farmer@123";

            var farmerUser = await userManager.FindByEmailAsync(farmerEmail);
            if (farmerUser == null)
            {
                farmerUser = new User
                {
                    Name = "Admin Farmer",
                    UserName = farmerEmail,
                    Email = farmerEmail,
                    EmailConfirmed = true,
                    Province = "Gauteng",
                    Department = "Crop Production"
                };

                var createFarmerResult = await userManager.CreateAsync(farmerUser, farmerPassword);
                if (createFarmerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser, "Farmer");
                }
            }

            // Create a default Employee user
            var employeeEmail = "employee.user@greengrid.com";
            var employeePassword = "Employee@1234";

            var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
            if (employeeUser == null)
            {
                employeeUser = new User
                {
                    Name = "Employee User",
                    UserName = employeeEmail,
                    Email = employeeEmail,
                    EmailConfirmed = true,
                    Province = "Western Cape",
                    Department = "Soil Science"
                };

                var createEmployeeResult = await userManager.CreateAsync(employeeUser, employeePassword);
                if (createEmployeeResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser, "Employee");
                }
            }
        }
    }
}
