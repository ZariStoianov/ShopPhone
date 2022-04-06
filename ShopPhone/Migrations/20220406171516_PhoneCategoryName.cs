using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopPhone.Migrations
{
    public partial class PhoneCategoryName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Categories_CategotyId",
                table: "Phones");

            migrationBuilder.RenameColumn(
                name: "CategotyId",
                table: "Phones",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_CategotyId",
                table: "Phones",
                newName: "IX_Phones_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Categories_CategoryId",
                table: "Phones",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Categories_CategoryId",
                table: "Phones");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Phones",
                newName: "CategotyId");

            migrationBuilder.RenameIndex(
                name: "IX_Phones_CategoryId",
                table: "Phones",
                newName: "IX_Phones_CategotyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Categories_CategotyId",
                table: "Phones",
                column: "CategotyId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
