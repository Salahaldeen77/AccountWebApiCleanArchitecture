namespace AccountWeb.Data.Entities.Procedures
{
    public class CountTransactionAccountByAccountIdProc
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public int CountTransaction { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class CountTransactionAccountByAccountIdProcParameters
    {
        public int AccountId { get; set; } = 0;
    }
}
