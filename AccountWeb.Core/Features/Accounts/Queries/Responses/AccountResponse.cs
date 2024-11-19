namespace AccountWeb.Core.Features.Accounts.Queries.Responses
{
    public class GetSingleAccountResponse
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        //public decimal OpeningBalance { get; set; }
        public decimal Balance { get; set; }

        public List<TransactionAccountRespone>? TransactionAccounts { get; set; }

        /// <summary>
        /// 0 Inactive,
        /// 1 Active
        /// </summary>
        public bool IsActive { get; set; }
        //public virtual ICollection<TransactionAccount> TransactionAccounts { get; set; }

    }
    public class TransactionAccountRespone
    {
        public int TransactionAccountId { get; set; }
        public Decimal Amount { get; set; }
        public string? TransactionType { get; set; }
        public int? TransferredToAccountId { get; set; }

    }

    public class GetAccountListResponse
    {
        public int Id { get; set; }
        //public int AccountNumber { get; set; }
        public string Name { get; set; }
        //public decimal OpeningBalance { get; set; }
        public decimal Balance { get; set; }

        /// <summary>
        /// 0 Inactive,
        /// 1 Active
        /// </summary>
        public bool IsActive { get; set; }
        //public virtual ICollection<TransactionAccount> TransactionAccounts { get; set; }


    }
}
