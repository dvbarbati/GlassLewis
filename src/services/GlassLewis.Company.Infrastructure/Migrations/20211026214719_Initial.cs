using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlassLewis.Company.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    StockTicker = table.Column<string>(type: "varchar(10)", nullable: false),
                    Exchange = table.Column<string>(type: "varchar(200)", nullable: false),
                    ISIN = table.Column<string>(type: "varchar(12)", nullable: false),
                    Website = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
