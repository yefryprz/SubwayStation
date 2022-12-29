using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubwayStation.API.Migrations.SubwayStation
{
    /// <inheritdoc />
    public partial class UpdateFrequently : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frequentlies_Subways_SubwayId",
                table: "Frequentlies");

            migrationBuilder.RenameColumn(
                name: "SubwayId",
                table: "Frequentlies",
                newName: "ObjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Frequentlies_SubwayId",
                table: "Frequentlies",
                newName: "IX_Frequentlies_ObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Frequentlies_Subways_ObjectId",
                table: "Frequentlies",
                column: "ObjectId",
                principalTable: "Subways",
                principalColumn: "ObjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frequentlies_Subways_ObjectId",
                table: "Frequentlies");

            migrationBuilder.RenameColumn(
                name: "ObjectId",
                table: "Frequentlies",
                newName: "SubwayId");

            migrationBuilder.RenameIndex(
                name: "IX_Frequentlies_ObjectId",
                table: "Frequentlies",
                newName: "IX_Frequentlies_SubwayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Frequentlies_Subways_SubwayId",
                table: "Frequentlies",
                column: "SubwayId",
                principalTable: "Subways",
                principalColumn: "ObjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
