using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Komitenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    Adresa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komitenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roba",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "text", nullable: false),
                    Cena = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roba", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dokumenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DatumIzdavanja = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KomitentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokumenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dokumenti_Komitenti_KomitentId",
                        column: x => x.KomitentId,
                        principalTable: "Komitenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Stavke",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CenaStavkeKom = table.Column<decimal>(type: "numeric", nullable: false),
                    Kolicina = table.Column<int>(type: "integer", nullable: false),
                    UkupnaCena = table.Column<decimal>(type: "numeric", nullable: false),
                    DokumentId = table.Column<int>(type: "integer", nullable: false),
                    RobaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stavke", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stavke_Dokumenti_DokumentId",
                        column: x => x.DokumentId,
                        principalTable: "Dokumenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stavke_Roba_RobaId",
                        column: x => x.RobaId,
                        principalTable: "Roba",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenti_KomitentId",
                table: "Dokumenti",
                column: "KomitentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_DokumentId",
                table: "Stavke",
                column: "DokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_RobaId",
                table: "Stavke",
                column: "RobaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stavke");

            migrationBuilder.DropTable(
                name: "Dokumenti");

            migrationBuilder.DropTable(
                name: "Roba");

            migrationBuilder.DropTable(
                name: "Komitenti");
        }
    }
}
