using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ClothesShop.Areas.Identity
{
    public class IdentityInitializer
    {
        private const string _adminEmail = "admin@gmail.com";
        private const string _adminPass = "AdminPassword2020!";

        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }

            if (await userManager.FindByNameAsync(_adminEmail) == null)
            {
                var admin = new IdentityUser() { Email = _adminEmail, UserName = _adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(admin, _adminPass);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }
}
