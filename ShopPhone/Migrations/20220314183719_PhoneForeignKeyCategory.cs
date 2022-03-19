using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopPhone.Migrations
{
    public partial class PhoneForeignKeyCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Categories_CategoryId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_CategoryId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Phones");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CategotyId",
                table: "Phones",
                column: "CategotyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Categories_CategotyId",
                table: "Phones",
                column: "CategotyId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Categories_CategotyId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_CategotyId",
                table: "Phones");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Phones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CategoryId",
                table: "Phones",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Categories_CategoryId",
                table: "Phones",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
