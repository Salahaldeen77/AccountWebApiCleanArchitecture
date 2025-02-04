using AccountWeb.Data.Entities;
using AccountWeb.Data.Enums;
using AccountWeb.Infrustructure.Abstracts;
using Microsoft.AspNetCore.Http;

namespace AccountWeb.Service.Abstracts
{
    public interface IAccountService : IMainRepository<Account>
    {
        public Task<Account> GetAccountByIdWithIncludeAsync(int id);
        public Task<Account> GetAccountByIdAsync(int id);
        public Task<Account> GetAccountByIdAsNoTrackingAsync(int id);
        public Task<string> AddAccountAsync(Account account, IFormFile file);
        public Task<bool> IsAccountNumberExist(int accountNumber);
        public Task<bool> IsAccountNumberExistExcludeSelf(int accountNumber, int Id);
        public Task<string> EditAccountAsync(Account account);
        public Task<string> DeleteAccountAsync(Account account);
        public Task<bool> IsAccountHaveTransactionByIdAsync(int id);
        public IQueryable<Account> GetAllAccountsQuerable();
        public IQueryable<Account> FilterAccountPaginatedQuerable(string search, AccountOrderingEnum orderingEnum = AccountOrderingEnum.Id);
        public Task<IEnumerable<Account>> GetAllAccountsWithIncludeAsync();

    }
}
