using AccountWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountWeb.Infrustructure.Configurations
{
    public class TransactionAccountConfigurations : IEntityTypeConfiguration<TransactionAccount>
    {
        public void Configure(EntityTypeBuilder<TransactionAccount> builder)
        {
            //Relationship One To Many TransactionAccount-Account
            builder.HasOne(transacc => transacc.Account)
            .WithMany(acc => acc.TransactionAccounts)
            .HasForeignKey(transacc => transacc.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

            //Relationship One To Many TransactionAccount-Transaction
            builder.HasOne(transacc => transacc.Transaction)
            .WithMany(trans => trans.TransactionAccounts)
            .HasForeignKey(transacc => transacc.TransactionId)
            .OnDelete(DeleteBehavior.Restrict);

            //one-one TransactionAccount-LedgerEntry
            builder.HasOne(t => t.LedgerEntry)
            .WithOne(l => l.TransactionAccount)
            .HasForeignKey<LedgerEntry>(l => l.TransactionAccountId);

        }
    }
}
