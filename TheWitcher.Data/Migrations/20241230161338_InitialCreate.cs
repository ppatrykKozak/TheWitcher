using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWitcher.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Postacie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    RasaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Poziom = table.Column<int>(type: "INTEGER", nullable: false),
                    Umiejetnosci = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postacie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rasy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rasy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ekwipunki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Typ = table.Column<string>(type: "TEXT", nullable: false),
                    PostacId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekwipunki", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ekwipunki_Postacie_PostacId",
                        column: x => x.PostacId,
                        principalTable: "Postacie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ekwipunki_PostacId",
                table: "Ekwipunki",
                column: "PostacId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ekwipunki");

            migrationBuilder.DropTable(
                name: "Rasy");

            migrationBuilder.DropTable(
                name: "Postacie");
        }
    }
}
