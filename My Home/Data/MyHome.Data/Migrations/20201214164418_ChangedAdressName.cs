using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHome.Data.Migrations
{
    public partial class ChangedAdressName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adrress",
                table: "Homes");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Homes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Homes");

            migrationBuilder.AddColumn<string>(
                name: "Adrress",
                table: "Homes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
