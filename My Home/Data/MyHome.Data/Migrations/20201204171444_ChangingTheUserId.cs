using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHome.Data.Migrations
{
    public partial class ChangingTheUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_AspNetUsers_AddedByUserIdId",
                table: "Homes");

            migrationBuilder.DropIndex(
                name: "IX_Homes_AddedByUserIdId",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "AddedByUser",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "AddedByUserIdId",
                table: "Homes");

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Homes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homes_AddedByUserId",
                table: "Homes",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_AspNetUsers_AddedByUserId",
                table: "Homes",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_AspNetUsers_AddedByUserId",
                table: "Homes");

            migrationBuilder.DropIndex(
                name: "IX_Homes_AddedByUserId",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Homes");

            migrationBuilder.AddColumn<string>(
                name: "AddedByUser",
                table: "Homes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserIdId",
                table: "Homes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homes_AddedByUserIdId",
                table: "Homes",
                column: "AddedByUserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_AspNetUsers_AddedByUserIdId",
                table: "Homes",
                column: "AddedByUserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
