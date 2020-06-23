using Microsoft.EntityFrameworkCore.Migrations;

namespace TelefonSatis.Migrations
{
    public partial class PhonePricesAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Phones",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinDesc",
                table: "Phones",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Phones",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "MinDesc",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Phones");
        }
    }
}
