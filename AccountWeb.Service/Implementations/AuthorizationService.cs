﻿using AccountWeb.Data.Entities.Identity;
using AccountWeb.Service.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace AccountWeb.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }


        #endregion
        #region Handle Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded) return "Success";

            return "Failed";
        }

        public async Task<bool> IsRoleExistAsync(string roleName)
        {
            //var role=await _roleManager.FindByNameAsync(roleName);
            //if (role != null) return true;
            //return false;
            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion
    }
}
