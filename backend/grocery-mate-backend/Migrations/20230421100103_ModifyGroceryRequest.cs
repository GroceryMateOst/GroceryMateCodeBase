using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifyGroceryRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListItem_Grocery_GroceryId",
                table: "ShoppingListItem");

            migrationBuilder.DropTable(
                name: "Grocery");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListItem_GroceryId",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "GroceryId",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ShoppingList");

            migrationBuilder.DropColumn(
                name: "PreferredStore",
                table: "ShoppingList");

            migrationBuilder.AddColumn<string>(
                name: "Grocery",
                table: "ShoppingListItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ShoppingListItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "GroceryRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "GroceryRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferredStore",
                table: "GroceryRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "GroceryRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grocery",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "GroceryRequests");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "GroceryRequests");

            migrationBuilder.DropColumn(
                name: "PreferredStore",
                table: "GroceryRequests");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "GroceryRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "GroceryId",
                table: "ShoppingListItem",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ShoppingList",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PreferredStore",
                table: "ShoppingList",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Grocery",
                columns: table => new
                {
                    GroceryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grocery", x => x.GroceryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_GroceryId",
                table: "ShoppingListItem",
                column: "GroceryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListItem_Grocery_GroceryId",
                table: "ShoppingListItem",
                column: "GroceryId",
                principalTable: "Grocery",
                principalColumn: "GroceryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
