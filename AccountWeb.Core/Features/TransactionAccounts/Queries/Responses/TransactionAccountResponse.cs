namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Responses
{
    public class GetTransactionAccountListResponse
    {
        public int Id { get; set; }

        //public int TransactionId { get; set; }

        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int AccounNumber { get; set; }
        public string TransactionType { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// 1 is debit,
        /// 0 is credit
        /// </summary>
        public bool IsDebit { get; set; }


        public int? TransferredToAccountId { get; set; }

        //public virtual Account Account { get; set; } = null!;

        //public virtual Transaction Transaction { get; set; } = null!;
        //public virtual LedgerEntry LedgerEntry { get; set; } = new LedgerEntry();

    }
}
