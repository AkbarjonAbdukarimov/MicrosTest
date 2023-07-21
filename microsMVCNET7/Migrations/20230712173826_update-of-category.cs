using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microsMVCNET7.Migrations
{
    /// <inheritdoc />
    public partial class updateofcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryValues_Categories_CategoryId",
                table: "CategoryValues");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryValues",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryValues_Categories_CategoryId",
                table: "CategoryValues",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryValues_Categories_CategoryId",
                table: "CategoryValues");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryValues",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryValues_Categories_CategoryId",
                table: "CategoryValues",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
