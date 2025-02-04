namespace AccountWeb.Data.Responses
{
    public class OperationsTransferAmountToThisAccountIdQueryResponse
    {
        public int TransferredToAccountId { get; set; }
        public int TransferredFromAccountId { get; set; }
        public decimal Amount { get; set; }

    }
}
