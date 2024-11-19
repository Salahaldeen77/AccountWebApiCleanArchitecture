using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountWeb.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class addFluentApiNoActionDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerEntries_TransactionsAccounts_TransactionAccountId",
                table: "LedgerEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsAccounts_Accounts_AccountId",
                table: "TransactionsAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsAccounts_Transactions_TransactionId",
                table: "TransactionsAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionsAccounts",
                table: "TransactionsAccounts");

            migrationBuilder.RenameTable(
                name: "TransactionsAccounts",
                newName: "TransactionAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionsAccounts_TransactionId",
                table: "TransactionAccounts",
                newName: "IX_TransactionAccounts_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionsAccounts_AccountId",
                table: "TransactionAccounts",
                newName: "IX_TransactionAccounts_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionAccounts",
                table: "TransactionAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerEntries_TransactionAccounts_TransactionAccountId",
                table: "LedgerEntries",
                column: "TransactionAccountId",
                principalTable: "TransactionAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionAccounts_Accounts_AccountId",
                table: "TransactionAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionAccounts_Transactions_TransactionId",
                table: "TransactionAccounts",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerEntries_TransactionAccounts_TransactionAccountId",
                table: "LedgerEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionAccounts_Accounts_AccountId",
                table: "TransactionAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionAccounts_Transactions_TransactionId",
                table: "TransactionAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionAccounts",
                table: "TransactionAccounts");

            migrationBuilder.RenameTable(
                name: "TransactionAccounts",
                newName: "TransactionsAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionAccounts_TransactionId",
                table: "TransactionsAccounts",
                newName: "IX_TransactionsAccounts_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionAccounts_AccountId",
                table: "TransactionsAccounts",
                newName: "IX_TransactionsAccounts_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionsAccounts",
                table: "TransactionsAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerEntries_TransactionsAccounts_TransactionAccountId",
                table: "LedgerEntries",
                column: "TransactionAccountId",
                principalTable: "TransactionsAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsAccounts_Accounts_AccountId",
                table: "TransactionsAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsAccounts_Transactions_TransactionId",
                table: "TransactionsAccounts",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
