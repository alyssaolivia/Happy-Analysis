using Microsoft.EntityFrameworkCore.Migrations;

namespace Happy_Analysis.Migrations
{
    public partial class No_ExpectedValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedValues",
                table: "Runs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpectedValues",
                table: "Runs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
