using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSifraPropertiesAndChangedForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumenti_Komitenti_KomitentId",
                table: "Dokumenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Stavke_Dokumenti_DokumentId",
                table: "Stavke");

            migrationBuilder.DropForeignKey(
                name: "FK_Stavke_Roba_RobaId",
                table: "Stavke");

            migrationBuilder.DropIndex(
                name: "IX_Stavke_DokumentId",
                table: "Stavke");

            migrationBuilder.DropIndex(
                name: "IX_Stavke_RobaId",
                table: "Stavke");

            migrationBuilder.DropIndex(
                name: "IX_Dokumenti_KomitentId",
                table: "Dokumenti");

            migrationBuilder.DropColumn(
                name: "CenaStavkeKom",
                table: "Stavke");

            migrationBuilder.DropColumn(
                name: "DokumentId",
                table: "Stavke");

            migrationBuilder.DropColumn(
                name: "RobaId",
                table: "Stavke");

            migrationBuilder.DropColumn(
                name: "KomitentId",
                table: "Dokumenti");

            migrationBuilder.AddColumn<string>(
                name: "BrojDokumenta",
                table: "Stavke",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DokumentEntityId",
                table: "Stavke",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SifraRobe",
                table: "Stavke",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SifraRobe",
                table: "Roba",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "Komitenti",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SifraKomitenta",
                table: "Komitenti",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrojDokumenta",
                table: "Dokumenti",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumDospeca",
                table: "Dokumenti",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SifraKomitenta",
                table: "Dokumenti",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Roba_SifraRobe",
                table: "Roba",
                column: "SifraRobe");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Komitenti_SifraKomitenta",
                table: "Komitenti",
                column: "SifraKomitenta");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Dokumenti_BrojDokumenta",
                table: "Dokumenti",
                column: "BrojDokumenta");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_BrojDokumenta",
                table: "Stavke",
                column: "BrojDokumenta");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_DokumentEntityId",
                table: "Stavke",
                column: "DokumentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_SifraRobe",
                table: "Stavke",
                column: "SifraRobe");

            migrationBuilder.CreateIndex(
                name: "IX_Roba_SifraRobe",
                table: "Roba",
                column: "SifraRobe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Komitenti_SifraKomitenta",
                table: "Komitenti",
                column: "SifraKomitenta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenti_BrojDokumenta",
                table: "Dokumenti",
                column: "BrojDokumenta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenti_SifraKomitenta",
                table: "Dokumenti",
                column: "SifraKomitenta");

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumenti_Komitenti_SifraKomitenta",
                table: "Dokumenti",
                column: "SifraKomitenta",
                principalTable: "Komitenti",
                principalColumn: "SifraKomitenta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stavke_Dokumenti_BrojDokumenta",
                table: "Stavke",
                column: "BrojDokumenta",
                principalTable: "Dokumenti",
                principalColumn: "BrojDokumenta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stavke_Dokumenti_DokumentEntityId",
                table: "Stavke",
                column: "DokumentEntityId",
                principalTable: "Dokumenti",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stavke_Roba_SifraRobe",
                table: "Stavke",
                column: "SifraRobe",
                principalTable: "Roba",
                principalColumn: "SifraRobe",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dokumenti_Komitenti_SifraKomitenta",
                table: "Dokumenti");

            migrationBuilder.DropForeignKey(
                name: "FK_Stavke_Dokumenti_BrojDokumenta",
                table: "Stavke");

            migrationBuilder.DropForeignKey(
                name: "FK_Stavke_Dokumenti_DokumentEntityId",
                table: "Stavke");

            migrationBuilder.DropForeignKey(
                name: "FK_Stavke_Roba_SifraRobe",
                table: "Stavke");

            migrationBuilder.DropIndex(
                name: "IX_Stavke_BrojDokumenta",
                table: "Stavke");

            migrationBuilder.DropIndex(
                name: "IX_Stavke_DokumentEntityId",
                table: "Stavke");

            migrationBuilder.DropIndex(
                name: "IX_Stavke_SifraRobe",
                table: "Stavke");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Roba_SifraRobe",
                table: "Roba");

            migrationBuilder.DropIndex(
                name: "IX_Roba_SifraRobe",
                table: "Roba");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Komitenti_SifraKomitenta",
                table: "Komitenti");

            migrationBuilder.DropIndex(
                name: "IX_Komitenti_SifraKomitenta",
                table: "Komitenti");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Dokumenti_BrojDokumenta",
                table: "Dokumenti");

            migrationBuilder.DropIndex(
                name: "IX_Dokumenti_BrojDokumenta",
                table: "Dokumenti");

            migrationBuilder.DropIndex(
                name: "IX_Dokumenti_SifraKomitenta",
                table: "Dokumenti");

            migrationBuilder.DropColumn(
                name: "BrojDokumenta",
                table: "Stavke");

            migrationBuilder.DropColumn(
                name: "DokumentEntityId",
                table: "Stavke");

            migrationBuilder.DropColumn(
                name: "SifraRobe",
                table: "Stavke");

            migrationBuilder.DropColumn(
                name: "SifraRobe",
                table: "Roba");

            migrationBuilder.DropColumn(
                name: "Grad",
                table: "Komitenti");

            migrationBuilder.DropColumn(
                name: "SifraKomitenta",
                table: "Komitenti");

            migrationBuilder.DropColumn(
                name: "BrojDokumenta",
                table: "Dokumenti");

            migrationBuilder.DropColumn(
                name: "DatumDospeca",
                table: "Dokumenti");

            migrationBuilder.DropColumn(
                name: "SifraKomitenta",
                table: "Dokumenti");

            migrationBuilder.RenameColumn(
                name: "UkupnaCenaStavke",
                table: "Stavke",
                newName: "UkupnaCena");

            migrationBuilder.AddColumn<decimal>(
                name: "CenaStavkeKom",
                table: "Stavke",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DokumentId",
                table: "Stavke",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RobaId",
                table: "Stavke",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KomitentId",
                table: "Dokumenti",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_DokumentId",
                table: "Stavke",
                column: "DokumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_RobaId",
                table: "Stavke",
                column: "RobaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dokumenti_KomitentId",
                table: "Dokumenti",
                column: "KomitentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dokumenti_Komitenti_KomitentId",
                table: "Dokumenti",
                column: "KomitentId",
                principalTable: "Komitenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stavke_Dokumenti_DokumentId",
                table: "Stavke",
                column: "DokumentId",
                principalTable: "Dokumenti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stavke_Roba_RobaId",
                table: "Stavke",
                column: "RobaId",
                principalTable: "Roba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
