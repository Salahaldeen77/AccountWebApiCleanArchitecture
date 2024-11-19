using AccountWeb.Data.Entities;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Context;
using AccountWeb.Infrustructure.Repositories;
using AccountWeb.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Service.Implementations
{
    public class TransactionAccountService : MainRepository<TransactionAccount>, ITransactionAccountService
    {
        // private readonly DbSet<TransactionAccount> _TransactionAccounts;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionAccountService(ApplicationDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
        {
            //  _TransactionAccounts = dbContext.Set<TransactionAccount>();
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AddTransactionAccountAsync(TransactionAccount transactionAccount)
        {

            if (_unitOfWork.TransactionAccounts.AddAsync(transactionAccount) == null)
                return "Failed";
            else
                return "Success";
        }

        public async Task<IEnumerable<TransactionAccount>> GetAllTransactionAccountWithIncludeAsync()
        {
            //return await _TransactionAccounts.Include(x => x.Account).Include(x=>x.Transaction).Include(x=>x.LedgerEntry).ToListAsync();
            return await _unitOfWork.TransactionAccounts.GetTableNoTracking().Include(x => x.Account).Include(x => x.Transaction).Include(x => x.LedgerEntry).ToListAsync();

        }

        public List<int> GetAllAccountId()
        {
            var AllAccounts = _unitOfWork.Accountes.FindAllAsync();
            List<int> AllId = new List<int>();

            for (int i = 0; i < AllAccounts.Result.Count(); i++)
            {
                AllId.Add(AllAccounts.Result.ElementAt(i).Id);
            }
            return AllId;
        }

        public async Task<bool> IsAccountExistByIdAsync(int accountId)
        {
            // return  _unitOfWork.Accountes.IsExistsById(accountId);
            var result = await _unitOfWork.Accountes.FindByIdAsync(accountId);
            if (result == null) return false;
            else return true;
        }

        public async Task<bool> IsTransactionExistByIdAsync(int transactionId)
        {
            var result = await _unitOfWork.Transactions.FindByIdAsync(transactionId);
            if (result == null) return false;
            else return true;
        }

        public async Task<string> PaymentAccountAsync(int accountId, decimal amount)
        {
            var account = await _unitOfWork.Accountes.FindByIdAsync(accountId);
            if (account == null) return "NotFound";

            account.OpeningBalance += amount;
            _unitOfWork.Accountes.Update(account);
            return "Success";
        }

        public async Task<string> ReceiptAccountAsync(int accountId, decimal amount)
        {
            var account = await _unitOfWork.Accountes.FindByIdAsync(accountId);
            if (account == null) return "NotFound";
            if (amount > account.OpeningBalance) return "Failed, Amount MoreThan Account Balance";

            account.OpeningBalance -= amount;
            _unitOfWork.Accountes.Update(account);
            return "Success";
        }

        public async Task<string> TransferAccountAsync(TransactionAccount transactionAccount)
        {

            var account = await _unitOfWork.Accountes.FindByIdAsync(transactionAccount.AccountId);
            var TransferredToAccount = await _unitOfWork.Accountes.FindByIdAsync(transactionAccount.TransferredToAccountId.Value);
            if (account == null || TransferredToAccount == null) return "On of the Accounts not found";

            using (var trans = _unitOfWork.Accountes.BeginTransaction())
            {
                try
                {


                    if (await ReceiptAccountAsync(transactionAccount.AccountId, transactionAccount.Amount) == "Success" &&
                        await PaymentAccountAsync(transactionAccount.TransferredToAccountId.Value, transactionAccount.Amount) == "Success"

                           )
                    {
                        await trans.CommitAsync();
                        return "Success";
                    }

                    else
                    {
                        await trans.RollbackAsync();
                        return "Failed";
                    }

                }
                catch
                {
                    await trans.RollbackAsync();
                    return "Failed";
                }
            }

        }
    }
}
