using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHome.Data.Migrations
{
    public partial class Favourites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavouriteId",
                table: "Homes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homes_FavouriteId",
                table: "Homes",
                column: "FavouriteId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_UserId",
                table: "Favourite",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Favourite_FavouriteId",
                table: "Homes",
                column: "FavouriteId",
                principalTable: "Favourite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Favourite_FavouriteId",
                table: "Homes");

            migrationBuilder.DropTable(
                name: "Favourite");

            migrationBuilder.DropIndex(
                name: "IX_Homes_FavouriteId",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FavouriteId",
                table: "Homes");
        }
    }
}
