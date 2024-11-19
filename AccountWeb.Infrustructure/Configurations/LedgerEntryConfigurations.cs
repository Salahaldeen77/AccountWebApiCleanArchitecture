using AccountWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountWeb.Infrustructure.Configurations
{
    public class LedgerEntryConfigurations : IEntityTypeConfiguration<LedgerEntry>
    {
        public void Configure(EntityTypeBuilder<LedgerEntry> builder)
        {
            //one-one LedgerEntry-TransactionAccount
            builder.HasOne(l => l.TransactionAccount)
            .WithOne(t => t.LedgerEntry)
            .HasForeignKey<LedgerEntry>(l => l.TransactionAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
