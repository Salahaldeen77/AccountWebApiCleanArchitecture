using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Requests;
using AccountWeb.Data.Responses;

namespace AccountWeb.Service.Abstracts
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByNameAsync(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int id);
        public Task<List<Role>> GetRolesListAsync();
        public Task<Role> GetRoleByIdAsync(int id);
        public Task<ManageUserRolesResponse> ManageUserRolesDataAsync(User user);
        public Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request);
        public Task<ManageUserClaimsResponse> ManageUserClaimsDataAsync(User user);
        public Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest request);
    }
}
