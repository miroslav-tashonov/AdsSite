using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localizations_Countries_CountryID",
                schema: "adsite",
                table: "Localizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Localizations_Languages_LanguageID",
                schema: "adsite",
                table: "Localizations");

            migrationBuilder.RenameColumn(
                name: "LanguageID",
                schema: "adsite",
                table: "Localizations",
                newName: "LanguageId");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                schema: "adsite",
                table: "Localizations",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Localizations_LanguageID",
                schema: "adsite",
                table: "Localizations",
                newName: "IX_Localizations_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Localizations_CountryID",
                schema: "adsite",
                table: "Localizations",
                newName: "IX_Localizations_CountryId");

            migrationBuilder.AlterColumn<string>(
                name: "LocalizationValue",
                schema: "adsite",
                table: "Localizations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LanguageId",
                schema: "adsite",
                table: "Localizations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                schema: "adsite",
                table: "Localizations",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                schema: "adsite",
                table: "AdDetailPictures",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizations_Countries_CountryId",
                schema: "adsite",
                table: "Localizations",
                column: "CountryId",
                principalSchema: "adsite",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizations_Languages_LanguageId",
                schema: "adsite",
                table: "Localizations",
                column: "LanguageId",
                principalSchema: "adsite",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localizations_Countries_CountryId",
                schema: "adsite",
                table: "Localizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Localizations_Languages_LanguageId",
                schema: "adsite",
                table: "Localizations");

            migrationBuilder.DropColumn(
                name: "File",
                schema: "adsite",
                table: "AdDetailPictures");

            migrationBuilder.RenameColumn(
                name: "LanguageId",
                schema: "adsite",
                table: "Localizations",
                newName: "LanguageID");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                schema: "adsite",
                table: "Localizations",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Localizations_LanguageId",
                schema: "adsite",
                table: "Localizations",
                newName: "IX_Localizations_LanguageID");

            migrationBuilder.RenameIndex(
                name: "IX_Localizations_CountryId",
                schema: "adsite",
                table: "Localizations",
                newName: "IX_Localizations_CountryID");

            migrationBuilder.AlterColumn<string>(
                name: "LocalizationValue",
                schema: "adsite",
                table: "Localizations",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "LanguageID",
                schema: "adsite",
                table: "Localizations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryID",
                schema: "adsite",
                table: "Localizations",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Localizations_Countries_CountryID",
                schema: "adsite",
                table: "Localizations",
                column: "CountryID",
                principalSchema: "adsite",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizations_Languages_LanguageID",
                schema: "adsite",
                table: "Localizations",
                column: "LanguageID",
                principalSchema: "adsite",
                principalTable: "Languages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
