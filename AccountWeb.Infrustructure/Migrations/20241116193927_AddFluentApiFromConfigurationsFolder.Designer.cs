﻿// <auto-generated />
using System;
using AccountWeb.Infrustructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountWeb.Infrustructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241116193927_AddFluentApiFromConfigurationsFolder")]
    partial class AddFluentApiFromConfigurationsFolder
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccountWeb.Data.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("OpeningBalance")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.LedgerEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDebit")
                        .HasColumnType("bit");

                    b.Property<int>("TransactionAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionAccountId")
                        .IsUnique();

                    b.ToTable("LedgerEntries");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ReferenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.TransactionAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDebit")
                        .HasColumnType("bit");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int?>("TransferredToAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionAccounts");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.LedgerEntry", b =>
                {
                    b.HasOne("AccountWeb.Data.Entities.TransactionAccount", "TransactionAccount")
                        .WithOne("LedgerEntry")
                        .HasForeignKey("AccountWeb.Data.Entities.LedgerEntry", "TransactionAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TransactionAccount");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.TransactionAccount", b =>
                {
                    b.HasOne("AccountWeb.Data.Entities.Account", "Account")
                        .WithMany("TransactionAccounts")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AccountWeb.Data.Entities.Transaction", "Transaction")
                        .WithMany("TransactionAccounts")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.Account", b =>
                {
                    b.Navigation("TransactionAccounts");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.Transaction", b =>
                {
                    b.Navigation("TransactionAccounts");
                });

            modelBuilder.Entity("AccountWeb.Data.Entities.TransactionAccount", b =>
                {
                    b.Navigation("LedgerEntry")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
