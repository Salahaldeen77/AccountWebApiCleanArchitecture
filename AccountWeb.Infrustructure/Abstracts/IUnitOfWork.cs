using AccountWeb.Data.Entities;
using AccountWeb.Data.Entities.Identity;

namespace AccountWeb.Infrustructure.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        IMainRepository<Account> Accountes { get; }
        IMainRepository<Transaction> Transactions { get; }
        IMainRepository<TransactionAccount> TransactionAccounts { get; }
        IMainRepository<LedgerEntry> LedgerEntries { get; }
        IMainRepository<UserRefreshToken> UserRefreshTokens { get; }
        int CommitChanges();
    }
}
