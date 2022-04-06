using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopPhone.Migrations
{
    public partial class PhoneColumnPriceForPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceForPhone",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceForPhone",
                table: "Phones");
        }
    }
}
