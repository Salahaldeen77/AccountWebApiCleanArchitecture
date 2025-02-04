using System.ComponentModel.DataAnnotations;

namespace AccountWeb.Data.Entities;

public partial class Account
{
    public Account()
    {
        TransactionAccounts = new HashSet<TransactionAccount>();
    }

    [Key]
    public int Id { get; set; }
    [Required]
    public int AccountNumber { get; set; }
    [StringLength(100)]
    [Required]
    public string Name { get; set; }
    //  public string NameAr { get; set; }
    [Required]
    public decimal OpeningBalance { get; set; }

    /// <summary>
    /// 0 Inactive,
    /// 1 Active
    /// </summary>
    [Required]
    public bool IsActive { get; set; }
    public string? Image { get; set; }

    public virtual ICollection<TransactionAccount> TransactionAccounts { get; set; }
}
