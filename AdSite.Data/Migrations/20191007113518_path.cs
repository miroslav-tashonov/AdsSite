using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations
{
    public partial class path : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                schema: "adsite",
                table: "Countries",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                schema: "adsite",
                table: "Countries");
        }
    }
}
