using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Happy_Analysis.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataPoints",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X1 = table.Column<int>(nullable: false),
                    X2 = table.Column<int>(nullable: false),
                    X3 = table.Column<int>(nullable: false),
                    X4 = table.Column<int>(nullable: false),
                    X5 = table.Column<int>(nullable: false),
                    X6 = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPoints", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Percentage1 = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Percentage2 = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Percentage3 = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Percentage4 = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Percentage5 = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Percentage6 = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Runs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ReferencePoint = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    F1_Score = table.Column<decimal>(type: "decimal(6,5)", nullable: false),
                    Run_Date = table.Column<DateTime>(nullable: false),
                    ExpectedValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataPoints");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Runs");
        }
    }
}
