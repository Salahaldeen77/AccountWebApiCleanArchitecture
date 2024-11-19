using AccountWeb.Data.Entities;
using AccountWeb.Infrustructure.Abstracts;

namespace AccountWeb.Service.Abstracts
{
    public interface ITransactionAccountService : IMainRepository<TransactionAccount>
    {
        public Task<string> AddTransactionAccountAsync(TransactionAccount transactionAccount);
        public Task<bool> IsAccountExistByIdAsync(int accountId);
        public Task<string> PaymentAccountAsync(int accountId, decimal amount);
        public Task<string> ReceiptAccountAsync(int accountId, decimal amount);
        public Task<string> TransferAccountAsync(TransactionAccount transactionAccount);
        public Task<bool> IsTransactionExistByIdAsync(int transactionId);
        public List<int> GetAllAccountId();
        public Task<IEnumerable<TransactionAccount>> GetAllTransactionAccountWithIncludeAsync();

    }
}
