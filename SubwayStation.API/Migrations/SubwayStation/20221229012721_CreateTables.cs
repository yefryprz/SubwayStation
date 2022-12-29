using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubwayStation.API.Migrations.SubwayStation
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geometrics",
                columns: table => new
                {
                    GeometricId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geometrics", x => x.GeometricId);
                });

            migrationBuilder.CreateTable(
                name: "Subways",
                columns: table => new
                {
                    ObjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeometricId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subways", x => x.ObjectId);
                    table.ForeignKey(
                        name: "FK_Subways_Geometrics_GeometricId",
                        column: x => x.GeometricId,
                        principalTable: "Geometrics",
                        principalColumn: "GeometricId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Frequentlies",
                columns: table => new
                {
                    FrequentlyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubwayId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequentlies", x => x.FrequentlyId);
                    table.ForeignKey(
                        name: "FK_Frequentlies_Subways_SubwayId",
                        column: x => x.SubwayId,
                        principalTable: "Subways",
                        principalColumn: "ObjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frequentlies_SubwayId",
                table: "Frequentlies",
                column: "SubwayId");

            migrationBuilder.CreateIndex(
                name: "IX_Subways_GeometricId",
                table: "Subways",
                column: "GeometricId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frequentlies");

            migrationBuilder.DropTable(
                name: "Subways");

            migrationBuilder.DropTable(
                name: "Geometrics");
        }
    }
}
