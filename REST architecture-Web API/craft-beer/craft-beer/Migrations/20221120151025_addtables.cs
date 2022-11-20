using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace craftbeer.Migrations
{
    /// <inheritdoc />
    public partial class addtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeerStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeerStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "beer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeerTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeerStyleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_beer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_beer_BeerStyles_BeerStyleId",
                        column: x => x.BeerStyleId,
                        principalTable: "BeerStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_beer_BeerStyleId",
                table: "beer",
                column: "BeerStyleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "beer");

            migrationBuilder.DropTable(
                name: "BeerStyles");
        }
    }
}
