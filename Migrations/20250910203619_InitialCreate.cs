using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VocabularyAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    English = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    French = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    CorrectCount = table.Column<int>(type: "INTEGER", nullable: false),
                    LastReviewed = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProgress_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProgress_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 10, 20, 36, 18, 907, DateTimeKind.Utc).AddTicks(2390), "bb@gmail.com", "test" },
                    { 2, new DateTime(2025, 9, 10, 20, 36, 18, 907, DateTimeKind.Utc).AddTicks(2810), "bbr@gmail.com", "test" }
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "Category", "English", "French" },
                values: new object[,]
                {
                    { 1, "animals", "cat", "chat" },
                    { 2, "animals", "dog", "chien" }
                });

            migrationBuilder.InsertData(
                table: "UserProgress",
                columns: new[] { "Id", "CorrectCount", "LastReviewed", "Level", "UserId", "WordId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 9, 10, 20, 36, 18, 907, DateTimeKind.Utc).AddTicks(3160), 1, 1, 1 },
                    { 2, 0, new DateTime(2025, 9, 10, 20, 36, 18, 907, DateTimeKind.Utc).AddTicks(3760), 0, 1, 2 },
                    { 3, 3, new DateTime(2025, 9, 10, 20, 36, 18, 907, DateTimeKind.Utc).AddTicks(3760), 2, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_UserId",
                table: "UserProgress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_WordId",
                table: "UserProgress",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
