using IdentityCheck.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("superUser@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "SuperUser",
                    Email = "superUser@gmail.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Almafa1234").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperUser").Wait();
                }

                var userCurrent = userManager.FindByEmailAsync("superUser@gmail.com").Result;

                if (!userManager.IsInRoleAsync(userCurrent, "SuperUser").Result)
                {
                    userManager.AddToRoleAsync(user, "SuperUser").Wait();
                }
            }
        }
    }
}
