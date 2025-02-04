﻿namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Responses
{
    public class GetViewTransactionAccountListResponse
    {
        public string TypeTransaction { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int? TransferredToAccountId { get; set; }
    }
}
