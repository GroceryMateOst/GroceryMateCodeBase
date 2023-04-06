using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Coordinate_CoordinateId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "ResidencyDetails",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "CoordinateId",
                table: "Address",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Coordinate_CoordinateId",
                table: "Address",
                column: "CoordinateId",
                principalTable: "Coordinate",
                principalColumn: "CoordinateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Coordinate_CoordinateId",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "ResidencyDetails",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CoordinateId",
                table: "Address",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Country",
                table: "Address",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Coordinate_CoordinateId",
                table: "Address",
                column: "CoordinateId",
                principalTable: "Coordinate",
                principalColumn: "CoordinateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
