using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microsMVCNET7.Migrations
{
    /// <inheritdoc />
    public partial class addedusertoreport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Reports",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AuthorId",
                table: "Reports",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                table: "Reports",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_AuthorId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_AuthorId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Reports");
        }
    }
}
