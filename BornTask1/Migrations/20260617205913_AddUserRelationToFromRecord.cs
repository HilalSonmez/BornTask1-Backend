using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BornTask1.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRelationToFromRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FormRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FormRecords_UserId",
                table: "FormRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormRecords_Users_UserId",
                table: "FormRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormRecords_Users_UserId",
                table: "FormRecords");

            migrationBuilder.DropIndex(
                name: "IX_FormRecords_UserId",
                table: "FormRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FormRecords");
        }
    }
}
