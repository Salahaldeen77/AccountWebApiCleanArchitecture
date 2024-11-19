using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountWeb.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteColumLocalizeArabic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumberAr",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OpeningBalanceAr",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "OpeningBalanceEn",
                table: "Accounts",
                newName: "OpeningBalance");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "Accounts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AccountNumberEn",
                table: "Accounts",
                newName: "AccountNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpeningBalance",
                table: "Accounts",
                newName: "OpeningBalanceEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Accounts",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "Accounts",
                newName: "AccountNumberEn");

            migrationBuilder.AddColumn<int>(
                name: "AccountNumberAr",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningBalanceAr",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
