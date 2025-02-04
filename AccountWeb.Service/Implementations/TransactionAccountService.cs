using AccountWeb.Data.Entities;
using AccountWeb.Data.Entities.Procedures;
using AccountWeb.Data.Entities.Views;
using AccountWeb.Data.Responses;
using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Abstracts.ProceduresAndFunctions;
using AccountWeb.Infrustructure.Context;
using AccountWeb.Infrustructure.Repositories;
using AccountWeb.Service.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AccountWeb.Service.Implementations
{
    public class TransactionAccountService : MainRepository<TransactionAccount>, ITransactionAccountService
    {
        // private readonly DbSet<TransactionAccount> _TransactionAccounts;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ITransactionAccountProcAndFuncRepository _transactionAccountProcAndFuncRepository;

        public TransactionAccountService(ApplicationDbContext dbContext, IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext,
                                         ITransactionAccountProcAndFuncRepository transactionAccountProcAndFuncRepository) : base(dbContext)
        {
            //  _TransactionAccounts = dbContext.Set<TransactionAccount>();
            _unitOfWork = unitOfWork;
            _applicationDbContext = applicationDbContext;
            _transactionAccountProcAndFuncRepository = transactionAccountProcAndFuncRepository;
        }

        public async Task<string> AddTransactionAccountAsync(TransactionAccount transactionAccount)
        {

            if (await _unitOfWork.TransactionAccounts.AddAsync(transactionAccount) == null)
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

        public async Task<string> PaymentAccountAsync(TransactionAccount transactionAccount)
        {
            using (var transact = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var account = await _unitOfWork.Accountes.FindByIdAsync(transactionAccount.AccountId);
                    if (account == null)
                        return "Account is Not Found";

                    account.OpeningBalance += transactionAccount.Amount;
                    _unitOfWork.Accountes.Update(account);

                    var addTransAccountResult = await AddTransactionAccountAsync(transactionAccount);
                    if (addTransAccountResult != "Success")
                        return "Failed Add TransactionAccount to database";

                    await transact.CommitAsync();
                    return "Success";
                }
                catch (Exception ex)
                {
                    await transact.RollbackAsync();
                    return "Failed in Payment Operation " + ex.Message;
                }
            }
        }

        public async Task<string> ReceiptAccountAsync(TransactionAccount transactionAccount)
        {
            using (var transact = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var account = await _unitOfWork.Accountes.FindByIdAsync(transactionAccount.AccountId);
                    if (account == null)
                        return "Account is Not Found";

                    if (transactionAccount.Amount > account.OpeningBalance)
                        return $"Failed, Amount Receipt  is More Than Account Balance {account.OpeningBalance}";

                    account.OpeningBalance -= transactionAccount.Amount;
                    _unitOfWork.Accountes.Update(account);

                    var addTransAccountResult = await AddTransactionAccountAsync(transactionAccount);
                    if (addTransAccountResult != "Success")
                        return "Failed Add TransactionAccount to database";

                    await transact.CommitAsync();
                    return "Success";
                }
                catch (Exception ex)
                {
                    await transact.RollbackAsync();
                    return "Failed in Receipt Operation " + ex.Message;
                }
            }

        }

        public async Task<string> TransferAccountAsync(TransactionAccount transactionAccount)
        {

            using (var trans = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    //get Account and TransferredToAccount 
                    var account = _unitOfWork.Accountes.FindById(transactionAccount.AccountId);
                    var TransferredToAccount = _unitOfWork.Accountes.FindById(transactionAccount.TransferredToAccountId.Value);
                    if (account == null || TransferredToAccount == null) return "One of the Accounts not found";

                    //receipt from Account
                    if (transactionAccount.Amount > account.OpeningBalance)
                        return $"Failed, Amount Receipt  is More Than Account Balance {account.OpeningBalance}";

                    account.OpeningBalance -= transactionAccount.Amount;
                    _unitOfWork.Accountes.Update(account);

                    //payment to TransferredToAccount
                    TransferredToAccount.OpeningBalance += transactionAccount.Amount;
                    _unitOfWork.Accountes.Update(TransferredToAccount);
                    //Add TransactionAccount to database
                    var addTransAccountResult = await AddTransactionAccountAsync(transactionAccount);
                    if (addTransAccountResult != "Success")
                        return "Failed Add TransactionAccount to database";
                    //
                    trans.Commit();
                    return "Success";
                    //
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return "Failed in transfer Operation " + ex.Message;
                }
            }

        }//

        public async Task<List<ViewTransAccounts>> GetViewTransAccountsAsync()
        {
            var viewTransAccount = await _unitOfWork.ViewTransAccounts.GetTableNoTracking().ToListAsync();
            return viewTransAccount;
        }

        public async Task<IReadOnlyList<CountTransactionAccountByAccountIdProc>> GetCountTransactionAccountByAccountIdProcsAsync(CountTransactionAccountByAccountIdProcParameters parameter)
        {
            var result = await _transactionAccountProcAndFuncRepository.GetCountTransactionAccountByAccountIdProcs(parameter);
            return result;
        }

        public Task<decimal> GetTotalAmountOfTransactionsAccountByAccountIdFuncAsync(int AccountId)
        {
            decimal totalAmount = -1;
            using (var cmd = _applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                totalAmount = _transactionAccountProcAndFuncRepository.GetTotalAmountOfTransactionsAccountByAccountIdFunc($"select dbo.GetTotalAmountOfTransAccountByAccountId({AccountId})", cmd);
            }
            return Task.FromResult(totalAmount);
        }

        public async Task<List<OperationsTransferAmountToThisAccountIdQueryResponse>> GetOperationsTransferAmountToThisAccountIdAsync(int AccountId)
        {
            using (var cmd = _applicationDbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                return await _transactionAccountProcAndFuncRepository.GetOperationsTransferAmountToThisAccountIdAsync($"select * from dbo.OperationsTransferAmountToThisAccount({AccountId})", cmd);
            }
        }
    }
}
