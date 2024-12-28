using AccountWeb.Data.DTOs;
using AccountWeb.Data.Entities.Identity;
using AccountWeb.Service.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace AccountWeb.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        public async Task<bool> IsRoleExistByNameAsync(string roleName)
        {
            //var role=await _roleManager.FindByNameAsync(roleName);
            //if (role != null) return true;
            //return false;
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null) return "NotFound";
            //Check If Name Exist Another Role
            var isNameExist = await _roleManager.FindByNameAsync(request.Name);
            if (isNameExist != null && isNameExist.Id != request.Id) return "NameIsExist";

            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";

            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<string> DeleteRoleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) return "NotFound";
            //check if users has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users != null && users.Count() > 0) return "Used";
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return "Success";
            var errors = string.Join("-", result.Errors);
            return errors;
        }

        #endregion
    }
}
