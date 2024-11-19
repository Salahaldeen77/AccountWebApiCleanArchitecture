using AccountWeb.Data.Entities;

namespace AccountWeb.Infrustructure.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        IMainRepository<Account> Accountes { get; }
        IMainRepository<Transaction> Transactions { get; }
        IMainRepository<TransactionAccount> TransactionAccounts { get; }
        IMainRepository<LedgerEntry> LedgerEntries { get; }
        int CommitChanges();
    }
}
