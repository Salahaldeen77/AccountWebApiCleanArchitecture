﻿using AccountWeb.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Infrustructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount <= 0)
            {
                await _roleManager.CreateAsync(new Role() { Name = "Admin" });
                await _roleManager.CreateAsync(new Role() { Name = "User" });
            }
        }
    }
}
