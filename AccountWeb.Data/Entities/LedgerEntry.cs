using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountWeb.Data.Entities
{
    public class LedgerEntry
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The TransactionAccountId is Required*_*")]
        public int TransactionAccountId  { get; set; }
        [Required(ErrorMessage = "The Amount is Required*_*")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }= DateTime.Now;

        public bool IsDebit { get; set; }

        [ForeignKey(nameof(TransactionAccountId))]
        public virtual TransactionAccount TransactionAccount { get; set; } = null!;



    }
}
