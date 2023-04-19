using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceCoordinate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Coordinate_CoordinateId",
                table: "Address");

            migrationBuilder.DropTable(
                name: "Coordinate");

            migrationBuilder.DropIndex(
                name: "IX_Address_CoordinateId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CoordinateId",
                table: "Address");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Address",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Address",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Address");

            migrationBuilder.AddColumn<Guid>(
                name: "CoordinateId",
                table: "Address",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coordinate",
                columns: table => new
                {
                    CoordinateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinate", x => x.CoordinateId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CoordinateId",
                table: "Address",
                column: "CoordinateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Coordinate_CoordinateId",
                table: "Address",
                column: "CoordinateId",
                principalTable: "Coordinate",
                principalColumn: "CoordinateId");
        }
    }
}
