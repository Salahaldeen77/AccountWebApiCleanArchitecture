using AccountWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountWeb.Infrustructure.Configurations
{
    public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            ////Relationship Many To one Transaction-TransactionAccount
            builder.HasMany(trans => trans.TransactionAccounts)
            .WithOne(transacc => transacc.Transaction)
            .HasForeignKey(transacc => transacc.TransactionId);




        }
    }
}
