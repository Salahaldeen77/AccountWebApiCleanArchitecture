using AccountWeb.Data.Entities;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Context;

namespace AccountWeb.Infrustructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Accountes = new MainRepository<Account>(_context);
            Transactions = new MainRepository<Transaction>(_context);
            TransactionAccounts = new MainRepository<TransactionAccount>(_context);
            LedgerEntries = new MainRepository<LedgerEntry>(_context);
        }
        public IMainRepository<Account> Accountes { get; private set; }

        public IMainRepository<Transaction> Transactions { get; private set; }

        public IMainRepository<TransactionAccount> TransactionAccounts { get; private set; }

        public IMainRepository<LedgerEntry> LedgerEntries { get; private set; }

        public int CommitChanges()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
