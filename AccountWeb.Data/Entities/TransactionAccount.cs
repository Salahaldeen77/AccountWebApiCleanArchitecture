using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountWeb.Data.Entities;

public partial class TransactionAccount
{
    [Key]
    public int Id { get; set; }

    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    /// <summary>
    /// 1 is debit,
    /// 0 is credit
    /// </summary>
    public bool IsDebit { get; set; }


    public int? TransferredToAccountId { get; set; }
    [ForeignKey(nameof(AccountId))]

    //[InverseProperty(nameof(Account.TransactionAccounts))]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey(nameof(TransactionId))]
    public virtual Transaction Transaction { get; set; } = null!;
    public virtual LedgerEntry LedgerEntry { get; set; }

}


