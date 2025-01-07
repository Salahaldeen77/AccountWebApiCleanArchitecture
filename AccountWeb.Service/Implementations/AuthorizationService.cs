using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Helpers;
using AccountWeb.Data.Requests;
using AccountWeb.Data.Responses;
using AccountWeb.Infrustructure.Context;
using AccountWeb.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AccountWeb.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        #endregion
        #region Constructors
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
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

        public async Task<List<Role>> GetRolesListAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<ManageUserRolesResponse> ManageUserRolesDataAsync(User user)
        {
            var response = new ManageUserRolesResponse();
            response.UserId = user.Id;
            var rolesListResponse = new List<UserRoles>();
            //get All Roles
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userRole.HasRole = true;
                else
                    userRole.HasRole = false;

                rolesListResponse.Add(userRole);
            }
            response.userRoles = rolesListResponse;
            return response;
        }

        public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //get user
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return "NotFound";
                //get user old roles
                var userRoles = await _userManager.GetRolesAsync(user);
                //delete old roles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldRoles";
                // Add New roles if HasRole=true
                var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);
                var addRolesResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesResult.Succeeded)
                    return "FailedToAddNewRoles";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }

        }

        public async Task<ManageUserClaimsResponse> ManageUserClaimsDataAsync(User user)
        {
            //create return response 
            var response = new ManageUserClaimsResponse();
            var userClaimsList = new List<UserClaims>();
            response.UserId = user.Id;
            //Get User Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in ClaimsStore.claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                    userClaim.Value = true;
                else
                    userClaim.Value = false;

                userClaimsList.Add(userClaim);
            }
            response.userClaims = userClaimsList;
            return response;

        }

        public async Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //get user
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return "NotFound";
                //get old user Claims
                var userClaims = await _userManager.GetClaimsAsync(user);
                //delete old User Claims
                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded)
                    return "FailedToRemoveOldClaims";

                // Add New Claims if Value=true
                var Claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var addClaimsResult = await _userManager.AddClaimsAsync(user, Claims);
                if (!addClaimsResult.Succeeded)
                    return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";

            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserClaims";
            }
        }

        #endregion
    }
}
