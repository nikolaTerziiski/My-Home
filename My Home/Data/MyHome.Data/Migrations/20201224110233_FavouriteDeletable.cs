using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHome.Data.Migrations
{
    public partial class FavouriteDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Favourite",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Favourite",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_IsDeleted",
                table: "Favourite",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Favourite_IsDeleted",
                table: "Favourite");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Favourite");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Favourite");
        }
    }
}
