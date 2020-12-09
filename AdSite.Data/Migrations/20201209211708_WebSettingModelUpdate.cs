using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations
{
    public partial class WebSettingModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "adsite",
                table: "WebSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoImagePath",
                schema: "adsite",
                table: "WebSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "adsite",
                table: "WebSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "adsite",
                table: "WebSettings");

            migrationBuilder.DropColumn(
                name: "LogoImagePath",
                schema: "adsite",
                table: "WebSettings");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "adsite",
                table: "WebSettings");
        }
    }
}
