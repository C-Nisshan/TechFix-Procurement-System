using Backend_TechFix.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_TechFix.Database
{
    public class SuperAdminSeeder
    {
        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, DatabaseContext context)
        {
            try
            {
                // Ensure Super Admin role exists
                if (!await roleManager.RoleExistsAsync("Super Admin"))
                {
                    var role = new IdentityRole<int> { Name = "Super Admin" };
                    var roleResult = await roleManager.CreateAsync(role);

                    if (!roleResult.Succeeded)
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            Console.WriteLine($"Error creating role: {error.Description}");
                        }
                    }
                }

                // Ensure UserType for Super Admin exists
                var superAdminUserType = context.UserTypes.SingleOrDefault(ut => ut.TypeName == "Super Admin");
                if (superAdminUserType == null)
                {
                    superAdminUserType = new UserType { TypeName = "Super Admin" };
                    await context.UserTypes.AddAsync(superAdminUserType);
                    await context.SaveChangesAsync();
                }

                // Check if the super admin user exists and create if it doesn't
                var superAdmin = await userManager.FindByNameAsync("superadmin");
                if (superAdmin == null)
                {
                    superAdmin = new User
                    {
                        UserName = "superadmin",
                        Email = "superadmin@techfix.com",
                        IsSuperAdmin = true,
                        UserTypeID = superAdminUserType.UserTypeID
                    };

                    var result = await userManager.CreateAsync(superAdmin, "InitialPassword123!");

                    if (result.Succeeded)
                    {
                        // Assign the Super Admin role to the user
                        await userManager.AddToRoleAsync(superAdmin, "Super Admin");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error creating super admin: {error.Description}");
                        }
                    }
                }
                else
                {
                    // Ensure existing superadmin has the correct role and UserTypeID
                    if (!await userManager.IsInRoleAsync(superAdmin, "Super Admin"))
                    {
                        await userManager.AddToRoleAsync(superAdmin, "Super Admin");
                    }
                    if (superAdmin.UserTypeID != superAdminUserType.UserTypeID)
                    {
                        superAdmin.UserTypeID = superAdminUserType.UserTypeID;
                        context.Users.Update(superAdmin);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during super admin seeding: {ex.Message}");
            }
        }
    }
}




