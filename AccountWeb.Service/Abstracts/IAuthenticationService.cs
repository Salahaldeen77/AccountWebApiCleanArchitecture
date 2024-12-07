using AccountWeb.Data.Entities.Identity;

namespace AccountWeb.Service.Abstracts
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);

    }
}
