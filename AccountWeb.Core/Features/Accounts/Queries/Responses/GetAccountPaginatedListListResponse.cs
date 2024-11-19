namespace AccountWeb.Core.Features.Accounts.Queries.Responses
{
    public class GetAccountPaginatedListListResponse
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        //public decimal OpeningBalance { get; set; }
        public decimal Balance { get; set; }

        /// <summary>
        /// 0 Inactive,
        /// 1 Active
        /// </summary>
        public bool IsActive { get; set; }
        public string? CountTransaction { get; set; }
        public GetAccountPaginatedListListResponse(int id, int accountNumber, string name, decimal balance, bool isActive, int countTransaction)
        {
            Id = id;
            AccountNumber = accountNumber;
            Name = name;
            Balance = balance;
            IsActive = isActive;
            CountTransaction = $"The number of his Transactions = {countTransaction}";
        }
    }
}
