using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    /// <inheritdoc />
    public partial class GroceryRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grocery",
                columns: table => new
                {
                    GroceryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grocery", x => x.GroceryId);
                });

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

            migrationBuilder.CreateTable(
                name: "ShoppingList",
                columns: table => new
                {
                    ShoppingListId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreferredStore = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingList", x => x.ShoppingListId);
                });

            migrationBuilder.CreateTable(
                name: "GroceryRequests",
                columns: table => new
                {
                    GroceryRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractorUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    RatingId = table.Column<Guid>(type: "uuid", nullable: true),
                    ShoppingListId = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroceryRequests", x => x.GroceryRequestId);
                    table.ForeignKey(
                        name: "FK_GroceryRequests_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "RatingId");
                    table.ForeignKey(
                        name: "FK_GroceryRequests_ShoppingList_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingList",
                        principalColumn: "ShoppingListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroceryRequests_User_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroceryRequests_User_ContractorUserId",
                        column: x => x.ContractorUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItem",
                columns: table => new
                {
                    ShoppingListItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroceryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    PreferredBrand = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    ShoppingListId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItem", x => x.ShoppingListItemId);
                    table.ForeignKey(
                        name: "FK_ShoppingListItem_Grocery_GroceryId",
                        column: x => x.GroceryId,
                        principalTable: "Grocery",
                        principalColumn: "GroceryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListItem_ShoppingList_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingList",
                        principalColumn: "ShoppingListId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroceryRequests_ClientUserId",
                table: "GroceryRequests",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroceryRequests_ContractorUserId",
                table: "GroceryRequests",
                column: "ContractorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroceryRequests_RatingId",
                table: "GroceryRequests",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_GroceryRequests_ShoppingListId",
                table: "GroceryRequests",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_EvaluatorUserId",
                table: "Rating",
                column: "EvaluatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_GroceryId",
                table: "ShoppingListItem",
                column: "GroceryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_ShoppingListId",
                table: "ShoppingListItem",
                column: "ShoppingListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroceryRequests");

            migrationBuilder.DropTable(
                name: "ShoppingListItem");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Grocery");

            migrationBuilder.DropTable(
                name: "ShoppingList");
        }
    }
}
