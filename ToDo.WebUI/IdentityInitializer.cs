using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ToDo.Entities.Concrete;

namespace ToDo.WebUI
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                AppRole role = new()
                {
                    Name = "Admin"
                };
                await roleManager.CreateAsync(role);
            }

            var memberRole = await roleManager.FindByNameAsync("Member");
            if (memberRole == null)
            {
                AppRole role = new()
                {
                    Name = "Member"
                };
                await roleManager.CreateAsync(role);
            }

            var adminUser = await userManager.FindByNameAsync("tanju");
            if (adminUser == null)
            {
                AppUser appUser = new()
                {
                    Name = "Tanju",
                    SurName = "Bozok",
                    UserName = "tanju",
                    Email = "tanju@gmail.com"
                };
                await userManager.CreateAsync(appUser, "12345*");
                await userManager.AddToRoleAsync(appUser, "Admin");
            }
        }
    }
}
