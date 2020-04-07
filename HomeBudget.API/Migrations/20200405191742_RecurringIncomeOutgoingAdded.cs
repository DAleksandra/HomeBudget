using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeBudget.API.Migrations
{
    public partial class RecurringIncomeOutgoingAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecurringIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    Interval = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringIncomes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecurringOutgoings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Shop = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Interval = table.Column<string>(nullable: true),
                    Cost = table.Column<float>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringOutgoings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringOutgoings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecurringIncomes_UserId",
                table: "RecurringIncomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecurringOutgoings_UserId",
                table: "RecurringOutgoings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurringIncomes");

            migrationBuilder.DropTable(
                name: "RecurringOutgoings");
        }
    }
}
