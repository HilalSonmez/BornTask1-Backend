using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BornTask1.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmailConfirmationExpireDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmationCodeExpireDate",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmationCodeExpireDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }
    }
}
