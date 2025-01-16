using AccountWeb.Data.Entities.Identity;

namespace AccountWeb.Service.Abstracts
{
    public interface IUserService
    {
        public Task<string> AddUserAsync(User user, string Password);
    }
}
