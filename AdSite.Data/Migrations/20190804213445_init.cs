using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "MainPictureThumbnailFile",
                schema: "adsite",
                table: "AdDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPictureThumbnailFile",
                schema: "adsite",
                table: "AdDetails");
        }
    }
}
