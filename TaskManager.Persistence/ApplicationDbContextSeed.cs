using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enumerations;
using TaskManager.Persistence.Identity;

namespace TaskManager.Persistence
{
    public sealed class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            const string password = "Smart2021#";

            var admin = new AppUser
            {
                UserName = "admin@smartsolutions.com",
                Email = "admin@smartsolutions.com",
                EmailConfirmed = true,
            };

            

            var adminEmployee = new Employee
            {
                FullName = "Mr. Admin",
                ShortName = "M.A.",
            };

            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToList();

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role.ToString()) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }

            if (await userManager.FindByEmailAsync(admin.Email) == null)
            {
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    var organization = new Organization
                    {
                        Name = "Test organization",
                        Phonenumber = "0999999999",
                        Address = "Test"
                    };
                    await context.Organizations.AddAsync(organization);
                    await context.SaveChangesAsync();

                    adminEmployee.AppUserId = admin.Id;
                    adminEmployee.OrganizationId = organization.Id;

                    await userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
                    await context.Employees.AddAsync(adminEmployee);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
