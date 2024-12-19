using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace AccountWeb.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(User user);
        public Task<JwtAuthResult> GetRefreshAccessToken(User user, string refreshToken, DateTime ExpiryDate);
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        public int GetUserId(JwtSecurityToken jwtToken);

    }
}
