using AccountWeb.Data.Entities.Procedures;
using AccountWeb.Data.Responses;
using AccountWeb.Infrustructure.Abstracts.ProceduresAndFunctions;
using AccountWeb.Infrustructure.Context;
using StoredProcedureEFCore;
using System.Data.Common;

namespace AccountWeb.Infrustructure.Repositories.ProceduresAndFunctions
{
    public class TransactionAccountProcAndFuncRepository : ITransactionAccountProcAndFuncRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructors
        public TransactionAccountProcAndFuncRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Handle Functions
        public async Task<IReadOnlyList<CountTransactionAccountByAccountIdProc>> GetCountTransactionAccountByAccountIdProcs(CountTransactionAccountByAccountIdProcParameters parameter)
        {
            var rows = new List<CountTransactionAccountByAccountIdProc>();
            await _context.LoadStoredProc(nameof(CountTransactionAccountByAccountIdProc))
                 .AddParam(nameof(CountTransactionAccountByAccountIdProcParameters.AccountId), parameter.AccountId)
                 .ExecAsync(async r => rows = await r.ToListAsync<CountTransactionAccountByAccountIdProc>());
            return rows;
        }

        public decimal GetTotalAmountOfTransactionsAccountByAccountIdFunc(string query, DbCommand command)
        {
            decimal totalAmount = -1;
            command.CommandText = query;
            var value = command.ExecuteScalar();
            var result = value.ToString();
            if (decimal.TryParse(result, out decimal d))
            {
                totalAmount = d;
            }

            return totalAmount;
        }

        public async Task<List<OperationsTransferAmountToThisAccountIdQueryResponse>> GetOperationsTransferAmountToThisAccountIdAsync(string query, DbCommand command)
        {
            command.CommandText = query;
            var reader = command.ExecuteReader();
            var result = await reader.ToListAsync<OperationsTransferAmountToThisAccountIdQueryResponse>();
            return result;
        }
        #endregion
    }
}
