using AccountWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountWeb.Infrustructure.Configurations
{
    public class AccountConfigurations : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //builder.HasKey(e => e.Id);
            //builder.Property(e => e.AccountNumber).IsRequired();
            //builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            //builder.Property(e => e.OpeningBalance).IsRequired();
            //builder.Property(e => e.IsActive).IsRequired();

            ////Relationship Many To one Account-TransactionAccount
            builder.HasMany(acc => acc.TransactionAccounts)
                .WithOne(transa => transa.Account)
                .HasForeignKey(trans => trans.AccountId);

        }
    }
}
