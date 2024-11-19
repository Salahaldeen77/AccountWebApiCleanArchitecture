using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountWeb.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class updateLedgerEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "LedgerEntries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "LedgerEntries");
        }
    }
}
