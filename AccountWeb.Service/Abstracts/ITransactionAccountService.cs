using AccountWeb.Data.Entities;
using AccountWeb.Data.Entities.Procedures;
using AccountWeb.Data.Entities.Views;
using AccountWeb.Data.Responses;
using AccountWeb.Infrustructure.Abstracts;
namespace AccountWeb.Service.Abstracts
{
    public interface ITransactionAccountService : IMainRepository<TransactionAccount>
    {
        public Task<string> AddTransactionAccountAsync(TransactionAccount transactionAccount);
        public Task<bool> IsAccountExistByIdAsync(int accountId);
        public Task<string> PaymentAccountAsync(TransactionAccount transactionAccount);
        public Task<string> ReceiptAccountAsync(TransactionAccount transactionAccount);
        public Task<string> TransferAccountAsync(TransactionAccount transactionAccount);
        public Task<bool> IsTransactionExistByIdAsync(int transactionId);
        public List<int> GetAllAccountId();
        public Task<IEnumerable<TransactionAccount>> GetAllTransactionAccountWithIncludeAsync();
        public Task<List<ViewTransAccounts>> GetViewTransAccountsAsync();
        public Task<IReadOnlyList<CountTransactionAccountByAccountIdProc>> GetCountTransactionAccountByAccountIdProcsAsync(CountTransactionAccountByAccountIdProcParameters parameter);
        public Task<decimal> GetTotalAmountOfTransactionsAccountByAccountIdFuncAsync(int AccountId);
        public Task<List<OperationsTransferAmountToThisAccountIdQueryResponse>> GetOperationsTransferAmountToThisAccountIdAsync(int AccountId);
    }
}
