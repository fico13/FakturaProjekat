using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedStanjePropertyForRoba : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stavke_Dokumenti_DokumentEntityId",
                table: "Stavke");

            migrationBuilder.DropIndex(
                name: "IX_Stavke_DokumentEntityId",
                table: "Stavke");

            migrationBuilder.DropColumn(
                name: "DokumentEntityId",
                table: "Stavke");

            migrationBuilder.AddColumn<decimal>(
                name: "Stanje",
                table: "Roba",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stanje",
                table: "Roba");

            migrationBuilder.AddColumn<int>(
                name: "DokumentEntityId",
                table: "Stavke",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stavke_DokumentEntityId",
                table: "Stavke",
                column: "DokumentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stavke_Dokumenti_DokumentEntityId",
                table: "Stavke",
                column: "DokumentEntityId",
                principalTable: "Dokumenti",
                principalColumn: "Id");
        }
    }
}
