﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountWeb.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "UserRefreshToken",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "UserRefreshToken");
        }
    }
}
