using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroceryRequests_Rating_RatingId",
                table: "GroceryRequests");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_GroceryRequests_RatingId",
                table: "GroceryRequests");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "PreferredBrand",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ShoppingListItem");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "GroceryRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ShoppingListItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ShoppingListItem",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreferredBrand",
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

            migrationBuilder.AddColumn<Guid>(
                name: "RatingId",
                table: "GroceryRequests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatorUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserRating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Rating_User_EvaluatorUserId",
                        column: x => x.EvaluatorUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroceryRequests_RatingId",
                table: "GroceryRequests",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_EvaluatorUserId",
                table: "Rating",
                column: "EvaluatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryRequests_Rating_RatingId",
                table: "GroceryRequests",
                column: "RatingId",
                principalTable: "Rating",
                principalColumn: "RatingId");
        }
    }
}
