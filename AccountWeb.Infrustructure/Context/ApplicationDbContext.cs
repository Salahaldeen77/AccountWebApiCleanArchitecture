using AccountWeb.Data.Entities;
using AccountWeb.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccountWeb.Infrustructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        // private readonly IEncryptionProvider _encryptionProvider;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //  _encryptionProvider = new GenerateEncryptionProvider("ca9ece9014c642dd863907768b5e84f3ca9ece9014c642dd863907768b5e84f3");
        }

        public DbSet<User> User { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionAccount> TransactionAccounts { get; set; }
        public DbSet<LedgerEntry> LedgerEntries { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api
            #region The code moved to Configurations Folder
            //modelBuilder.Entity<Account>(entity =>
            //{
            //    //entity.HasKey(e => e.Id);
            //    //entity.Property(e => e.AccountNumber).IsRequired();
            //    //entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            //    //entity.Property(e => e.OpeningBalance).IsRequired();
            //    //entity.Property(e => e.IsActive).IsRequired();

            //    ////Relationship Many To one Account-TransactionAccount
            //    entity.HasMany(acc => acc.TransactionAccounts)
            //    .WithOne(transa => transa.Account)
            //    .HasForeignKey(trans => trans.AccountId);

            //});

            //modelBuilder.Entity<Transaction>(entity =>
            //{
            //    ////Relationship Many To one Transaction-TransactionAccount
            //    entity.HasMany(trans => trans.TransactionAccounts)
            //    .WithOne(transacc => transacc.Transaction)
            //    .HasForeignKey(transacc => transacc.TransactionId);

            //});


            //modelBuilder.Entity<TransactionAccount>(entity =>
            //{
            //    //Relationship One To Many TransactionAccount-Account
            //    entity.HasOne(transacc => transacc.Account)
            //    .WithMany(acc => acc.TransactionAccounts)
            //    .HasForeignKey(transacc => transacc.AccountId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //    //Relationship One To Many TransactionAccount-Transaction
            //    entity.HasOne(transacc => transacc.Transaction)
            //    .WithMany(trans => trans.TransactionAccounts)
            //    .HasForeignKey(transacc => transacc.TransactionId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //    //one-one TransactionAccount-LedgerEntry
            //    entity.HasOne(t => t.LedgerEntry)
            //    .WithOne(l => l.TransactionAccount)
            //    .HasForeignKey<LedgerEntry>(l => l.TransactionAccountId);
            //});


            //modelBuilder.Entity<LedgerEntry>(entity =>
            //{
            //    //one-one LedgerEntry-TransactionAccount
            //    entity.HasOne(l => l.TransactionAccount)
            //    .WithOne(t => t.LedgerEntry)
            //    .HasForeignKey<LedgerEntry>(l => l.TransactionAccountId)
            //    .OnDelete(DeleteBehavior.Restrict);
            //});
            #endregion



            //Call and Apply any configurations in solution
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.UseEncryption(_encryptionProvider);
            var encryptionConverter = new EncryptionConverter("ca9ece9014c642dd863907768b5e84f3ca9ece9014c642dd863907768b5e84f3");
            modelBuilder.Entity<User>()
                .Property(u => u.Code)
                .HasConversion(encryptionConverter);

            base.OnModelCreating(modelBuilder);
        }
    }
}
