using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations
{
    public partial class urc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleCountry_AspNetRoles_ApplicationIdentityRoleId",
                schema: "adsite",
                table: "UserRoleCountry");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleCountry_AspNetUsers_ApplicationUserId",
                schema: "adsite",
                table: "UserRoleCountry");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleCountry_Countries_CountryId",
                schema: "adsite",
                table: "UserRoleCountry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleCountry",
                schema: "adsite",
                table: "UserRoleCountry");

            migrationBuilder.RenameTable(
                name: "UserRoleCountry",
                schema: "adsite",
                newName: "UserRoleCountries",
                newSchema: "adsite");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleCountry_CountryId",
                schema: "adsite",
                table: "UserRoleCountries",
                newName: "IX_UserRoleCountries_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleCountry_ApplicationUserId",
                schema: "adsite",
                table: "UserRoleCountries",
                newName: "IX_UserRoleCountries_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleCountry_ApplicationIdentityRoleId",
                schema: "adsite",
                table: "UserRoleCountries",
                newName: "IX_UserRoleCountries_ApplicationIdentityRoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleCountries",
                schema: "adsite",
                table: "UserRoleCountries",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleCountries_AspNetRoles_ApplicationIdentityRoleId",
                schema: "adsite",
                table: "UserRoleCountries",
                column: "ApplicationIdentityRoleId",
                principalSchema: "adsite",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleCountries_AspNetUsers_ApplicationUserId",
                schema: "adsite",
                table: "UserRoleCountries",
                column: "ApplicationUserId",
                principalSchema: "adsite",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleCountries_Countries_CountryId",
                schema: "adsite",
                table: "UserRoleCountries",
                column: "CountryId",
                principalSchema: "adsite",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleCountries_AspNetRoles_ApplicationIdentityRoleId",
                schema: "adsite",
                table: "UserRoleCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleCountries_AspNetUsers_ApplicationUserId",
                schema: "adsite",
                table: "UserRoleCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleCountries_Countries_CountryId",
                schema: "adsite",
                table: "UserRoleCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleCountries",
                schema: "adsite",
                table: "UserRoleCountries");

            migrationBuilder.RenameTable(
                name: "UserRoleCountries",
                schema: "adsite",
                newName: "UserRoleCountry",
                newSchema: "adsite");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleCountries_CountryId",
                schema: "adsite",
                table: "UserRoleCountry",
                newName: "IX_UserRoleCountry_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleCountries_ApplicationUserId",
                schema: "adsite",
                table: "UserRoleCountry",
                newName: "IX_UserRoleCountry_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleCountries_ApplicationIdentityRoleId",
                schema: "adsite",
                table: "UserRoleCountry",
                newName: "IX_UserRoleCountry_ApplicationIdentityRoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleCountry",
                schema: "adsite",
                table: "UserRoleCountry",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleCountry_AspNetRoles_ApplicationIdentityRoleId",
                schema: "adsite",
                table: "UserRoleCountry",
                column: "ApplicationIdentityRoleId",
                principalSchema: "adsite",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleCountry_AspNetUsers_ApplicationUserId",
                schema: "adsite",
                table: "UserRoleCountry",
                column: "ApplicationUserId",
                principalSchema: "adsite",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleCountry_Countries_CountryId",
                schema: "adsite",
                table: "UserRoleCountry",
                column: "CountryId",
                principalSchema: "adsite",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
