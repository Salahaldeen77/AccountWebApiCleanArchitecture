using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountWeb.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class editTableLedgerEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDebit",
                table: "LedgerEntries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDebit",
                table: "LedgerEntries");
        }
    }
}
