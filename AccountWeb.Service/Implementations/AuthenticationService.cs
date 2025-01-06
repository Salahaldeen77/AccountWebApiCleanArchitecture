using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Helpers;
using AccountWeb.Data.Responses;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AccountWeb.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user); //using table function to return more than one type
            var refreshToken = GetRefreshToken(user.UserName);

            var userRefreshToken = new UserRefreshToken()
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id,
            };
            await _unitOfWork.UserRefreshTokens.AddAsync(userRefreshToken); //Add userRefreshToken to database

            var response = new JwtAuthResult();
            response.AccessToken = accessToken;
            response.refreshToken = refreshToken;
            return response;
        }
        public async Task<JwtAuthResult> GetRefreshAccessToken(User user, string refreshToken, DateTime ExpiryDate)
        {

            //#Generate Refresh AccessToken
            var (newjwtSecurtyToken, newToken) = await GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = user.UserName;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = ExpiryDate;
            response.refreshToken = refreshTokenResult;
            return response;
        }
        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
                if (validator == null)
                {
                    return ("InvalidToken");
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {


            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWorng", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow) //check if ExpairedTime > TimeNow 
            {
                return ("TokenIsNotExpired", null);
            }

            //#Get userRefreshToken
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            var userRefreshToken = await _unitOfWork.UserRefreshTokens.GetTableNoTracking()
                                                                 .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                                      x.RefreshToken == refreshToken &&
                                                                                      x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }
            var expireDate = userRefreshToken.ExpiryDate;

            //#Validation RefreshToken 
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                _unitOfWork.UserRefreshTokens.Update(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }

            return ("Success", expireDate);

        }
        public int GetUserId(JwtSecurityToken jwtToken)
        {
            //#Get userId
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            return int.Parse(userId);
        }
        #endregion

        #region Functions

        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = userName,
                TokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private List<Claim> GetClaims(User user, List<string> roles)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
                new Claim("UserId",user.Id.ToString()),
            };
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return Claims;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = GetClaims(user, roles.ToList());
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }


        #endregion


    }
}
