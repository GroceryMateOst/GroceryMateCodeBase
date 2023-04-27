using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    /// <inheritdoc />
    public partial class TokenBlacklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanceledTokens",
                columns: table => new
                {
                    TokenBlacklistEntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CanceledToken = table.Column<string>(type: "text", nullable: false),
                    CancellationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanceledTokens", x => x.TokenBlacklistEntryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanceledTokens");
        }
    }
}
