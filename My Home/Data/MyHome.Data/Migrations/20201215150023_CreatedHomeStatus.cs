using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHome.Data.Migrations
{
    public partial class CreatedHomeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Homes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Homes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Homes");
        }
    }
}
