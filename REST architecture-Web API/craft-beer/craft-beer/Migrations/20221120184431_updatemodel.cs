using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace craftbeer.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_beer_BeerStyles_BeerStyleId",
                table: "beer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_beer",
                table: "beer");

            migrationBuilder.RenameTable(
                name: "beer",
                newName: "Beers");

            migrationBuilder.RenameIndex(
                name: "IX_beer_BeerStyleId",
                table: "Beers",
                newName: "IX_Beers_BeerStyleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beers",
                table: "Beers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_BeerStyles_BeerStyleId",
                table: "Beers",
                column: "BeerStyleId",
                principalTable: "BeerStyles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_BeerStyles_BeerStyleId",
                table: "Beers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beers",
                table: "Beers");

            migrationBuilder.RenameTable(
                name: "Beers",
                newName: "beer");

            migrationBuilder.RenameIndex(
                name: "IX_Beers_BeerStyleId",
                table: "beer",
                newName: "IX_beer_BeerStyleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_beer",
                table: "beer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_beer_BeerStyles_BeerStyleId",
                table: "beer",
                column: "BeerStyleId",
                principalTable: "BeerStyles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
