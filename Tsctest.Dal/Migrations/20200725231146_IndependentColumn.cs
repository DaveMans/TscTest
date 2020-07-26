using Microsoft.EntityFrameworkCore.Migrations;

namespace Tsctest.Dal.Migrations
{
    public partial class IndependentColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Independent",
                table: "Countries",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Independent",
                table: "Countries");
        }
    }
}
