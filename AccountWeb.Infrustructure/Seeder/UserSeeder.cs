using AccountWeb.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Infrustructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "admin",
                    Email = "admin@gmailcom",
                    FullName = "AccountProject",
                    Country = "yemen",
                    PhoneNumber = "777988307",
                    Address = "sanaa",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultUser, "1234");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
