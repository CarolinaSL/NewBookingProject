using Microsoft.AspNetCore.Identity;
using NewBookingApp.Core.EFCore;
using NewBookingApp.Identity.Models;
using NewBookingApp.Identity.Models.Constants;

namespace NewBookingApp.Identity.Data.Configuration
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityDataSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAllAsync()
        {
            await SeedRoles();
            await SeedUsers();
        }

        private async Task SeedRoles()
        {
            if (await _roleManager.RoleExistsAsync(Constants.Role.Admin) == false)
                await _roleManager.CreateAsync(new(Constants.Role.Admin));

            if (await _roleManager.RoleExistsAsync(Constants.Role.User) == false)
                await _roleManager.CreateAsync(new(Constants.Role.User));
        }

        private async Task SeedUsers()
        {
            if (await _userManager.FindByNameAsync("carol") == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Carolina",
                    LastName = "Louzada",
                    UserName = "carol",
                    Email = "carolina.louzada@hotmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PassPortNumber = "123456789"
                };

                var result = await _userManager.CreateAsync(user, "Admin@123456");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, Constants.Role.Admin);
            }

            if (await _userManager.FindByNameAsync("carol2") == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Carolina",
                    LastName = "L",
                    UserName = "carol2",
                    Email = "carol2@test.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PassPortNumber = "12356513877"
                };

                var result = await _userManager.CreateAsync(user, "User@123456");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, Constants.Role.User);
            }
        }
    }
}
