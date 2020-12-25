using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHome.Data.Migrations
{
    public partial class Favourite2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteHome");

            migrationBuilder.AddColumn<int>(
                name: "FavouriteId",
                table: "Homes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homes_FavouriteId",
                table: "Homes",
                column: "FavouriteId");

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

            migrationBuilder.DropIndex(
                name: "IX_Homes_FavouriteId",
                table: "Homes");

            migrationBuilder.DropColumn(
                name: "FavouriteId",
                table: "Homes");

            migrationBuilder.CreateTable(
                name: "FavouriteHome",
                columns: table => new
                {
                    FavouritesId = table.Column<int>(type: "int", nullable: false),
                    HomesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteHome", x => new { x.FavouritesId, x.HomesId });
                    table.ForeignKey(
                        name: "FK_FavouriteHome_Favourite_FavouritesId",
                        column: x => x.FavouritesId,
                        principalTable: "Favourite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteHome_Homes_HomesId",
                        column: x => x.HomesId,
                        principalTable: "Homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteHome_HomesId",
                table: "FavouriteHome",
                column: "HomesId");
        }
    }
}
