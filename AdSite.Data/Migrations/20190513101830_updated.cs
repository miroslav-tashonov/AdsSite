using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                schema: "adsite",
                table: "Wishlists",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CountryId",
                schema: "adsite",
                table: "Wishlists",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Countries_CountryId",
                schema: "adsite",
                table: "Wishlists",
                column: "CountryId",
                principalSchema: "adsite",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Countries_CountryId",
                schema: "adsite",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_CountryId",
                schema: "adsite",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "CountryId",
                schema: "adsite",
                table: "Wishlists");
        }
    }
}
