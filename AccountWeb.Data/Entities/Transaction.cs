using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Data.Entities;

public partial class Transaction
{
    public Transaction()
    {
        TransactionAccounts = new HashSet<TransactionAccount>();
    }

    /// <summary>
    /// 1 payment,
    /// 2 receipt,
    /// 3 transfer
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// 1 payment,
    /// 2 receipt,
    /// 3 transfer
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Type { get; set; } = null!;

    public int? ReferenceNumber { get; set; }

    public virtual ICollection<TransactionAccount> TransactionAccounts { get; set; }
}
