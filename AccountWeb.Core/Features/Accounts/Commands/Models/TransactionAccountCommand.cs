using AccountWeb.Core.Bases;
using MediatR;

namespace AccountWeb.Core.Features.Accounts.Commands.Models
{
    public class AddTransactionAccountCommand : IRequest<Response<string>>
    {

        public int TransactionId { get; set; }

        public int AccountId { get; set; }
        public decimal Amount { get; set; }

        /// <summary>
        /// 1 is debit,
        /// 0 is credit
        /// </summary>

        // public bool IsDebit { get; set; }


        public int? TransferredToAccountId { get; set; }

        //[ForeignKey(nameof(AccountId))]
        //public  Account Account { get; set; } = null!;

        //[ForeignKey(nameof(TransactionId))]
        //public  Transaction Transaction { get; set; } = null!;
        //public  LedgerEntry LedgerEntry { get; set; } 
    }
}
