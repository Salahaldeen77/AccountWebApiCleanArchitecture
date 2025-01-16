using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Responses;
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
        public Task<string> ConfirmEmail(int? userId, string? code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ConfirmResetPassword(string Code, string Email);
        public Task<string> ResetPassword(string Email, string Password);

    }
}
