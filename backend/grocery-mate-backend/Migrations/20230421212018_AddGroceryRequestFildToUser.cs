using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddGroceryRequestFildToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroceryRequests_User_ClientUserId",
                table: "GroceryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_GroceryRequests_User_ContractorUserId",
                table: "GroceryRequests");

            migrationBuilder.DropIndex(
                name: "IX_GroceryRequests_ClientUserId",
                table: "GroceryRequests");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "GroceryRequests");

            migrationBuilder.RenameColumn(
                name: "ContractorUserId",
                table: "GroceryRequests",
                newName: "GroceryRequestsClients");

            migrationBuilder.RenameIndex(
                name: "IX_GroceryRequests_ContractorUserId",
                table: "GroceryRequests",
                newName: "IX_GroceryRequests_GroceryRequestsClients");

            migrationBuilder.AddColumn<Guid>(
                name: "GroceryRequestsContractors",
                table: "GroceryRequests",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroceryRequests_GroceryRequestsContractors",
                table: "GroceryRequests",
                column: "GroceryRequestsContractors");

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryRequests_User_GroceryRequestsClients",
                table: "GroceryRequests",
                column: "GroceryRequestsClients",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryRequests_User_GroceryRequestsContractors",
                table: "GroceryRequests",
                column: "GroceryRequestsContractors",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroceryRequests_User_GroceryRequestsClients",
                table: "GroceryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_GroceryRequests_User_GroceryRequestsContractors",
                table: "GroceryRequests");

            migrationBuilder.DropIndex(
                name: "IX_GroceryRequests_GroceryRequestsContractors",
                table: "GroceryRequests");

            migrationBuilder.DropColumn(
                name: "GroceryRequestsContractors",
                table: "GroceryRequests");

            migrationBuilder.RenameColumn(
                name: "GroceryRequestsClients",
                table: "GroceryRequests",
                newName: "ContractorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_GroceryRequests_GroceryRequestsClients",
                table: "GroceryRequests",
                newName: "IX_GroceryRequests_ContractorUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientUserId",
                table: "GroceryRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GroceryRequests_ClientUserId",
                table: "GroceryRequests",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryRequests_User_ClientUserId",
                table: "GroceryRequests",
                column: "ClientUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryRequests_User_ContractorUserId",
                table: "GroceryRequests",
                column: "ContractorUserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
