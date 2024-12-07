using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Helpers;
using AccountWeb.Service.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountWeb.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        #endregion

        #region Handle Functions
        public Task<string> GetJWTToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserName",user.UserName),
                new Claim("Email",user.Email),
                new Claim("PhoneNumber",user.PhoneNumber),
            };
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(accessToken);
        }
        #endregion


    }
}
