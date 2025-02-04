using AccountWeb.Data.Entities.Procedures;
using AccountWeb.Data.Responses;
using System.Data.Common;

namespace AccountWeb.Infrustructure.Abstracts.ProceduresAndFunctions
{
    public interface ITransactionAccountProcAndFuncRepository
    {
        public Task<IReadOnlyList<CountTransactionAccountByAccountIdProc>> GetCountTransactionAccountByAccountIdProcs(CountTransactionAccountByAccountIdProcParameters parameter);
        public decimal GetTotalAmountOfTransactionsAccountByAccountIdFunc(string query, DbCommand command);
        public Task<List<OperationsTransferAmountToThisAccountIdQueryResponse>> GetOperationsTransferAmountToThisAccountIdAsync(string query, DbCommand command);
    }
}
