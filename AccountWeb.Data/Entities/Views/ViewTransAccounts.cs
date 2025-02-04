using Microsoft.EntityFrameworkCore;

namespace AccountWeb.Data.Entities.Views
{
    [Keyless]
    public class ViewTransAccounts
    {
        public string TypeTransaction { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int? TransferredToAccountId { get; set; }
    }
}
