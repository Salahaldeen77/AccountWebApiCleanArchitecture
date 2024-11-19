using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountWeb.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalizerArToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpeningBalance",
                table: "Accounts",
                newName: "OpeningBalanceEn");

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

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Accounts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OpeningBalanceAr",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumberAr",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OpeningBalanceAr",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "OpeningBalanceEn",
                table: "Accounts",
                newName: "OpeningBalance");

            migrationBuilder.RenameColumn(
                name: "AccountNumberEn",
                table: "Accounts",
                newName: "AccountNumber");
        }
    }
}
