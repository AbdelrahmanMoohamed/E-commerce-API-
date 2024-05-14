using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Reposatory.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUSerAsunc(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Abdelrahman Mohamed",
                    Email = "abdelrahman.route@gmail.com",
                    UserName = "abdelrahman.route",
                    PhoneNumber = "1234567890",
                };
                await userManager.CreateAsync(User , "Pa$$word");
            }
        }
    }
}
