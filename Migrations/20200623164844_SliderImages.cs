using Microsoft.EntityFrameworkCore.Migrations;

namespace TelefonSatis.Migrations
{
    public partial class SliderImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SliderImages",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderImages",
                table: "Settings");
        }
    }
}
