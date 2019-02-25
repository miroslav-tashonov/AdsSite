using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations.AdSiteDb
{
    public partial class adsite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "adsite");

            migrationBuilder.CreateTable(
                name: "AdDetails",
                schema: "adsite",
                columns: table => new
                {
                    AdDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    AdID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdDetails", x => x.AdDetailID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "adsite",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "AdDetailPictures",
                schema: "adsite",
                columns: table => new
                {
                    AdDetailPictureID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdDetailID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdDetailPictures", x => x.AdDetailPictureID);
                    table.ForeignKey(
                        name: "FK_AdDetailPictures_AdDetails_AdDetailID",
                        column: x => x.AdDetailID,
                        principalSchema: "adsite",
                        principalTable: "AdDetails",
                        principalColumn: "AdDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "adsite",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_Categories_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "adsite",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                schema: "adsite",
                columns: table => new
                {
                    AdID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    CityID = table.Column<int>(nullable: false),
                    CountryID = table.Column<int>(nullable: false),
                    AdDetailID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.AdID);
                    table.ForeignKey(
                        name: "FK_Ads_AdDetails_AdID",
                        column: x => x.AdID,
                        principalSchema: "adsite",
                        principalTable: "AdDetails",
                        principalColumn: "AdDetailID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ads_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "adsite",
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ads_Cities_CityID",
                        column: x => x.CityID,
                        principalSchema: "adsite",
                        principalTable: "Cities",
                        principalColumn: "CityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ads_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdDetailPictures_AdDetailID",
                schema: "adsite",
                table: "AdDetailPictures",
                column: "AdDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CategoryID",
                schema: "adsite",
                table: "Ads",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CityID",
                schema: "adsite",
                table: "Ads",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Ads_CountryID",
                schema: "adsite",
                table: "Ads",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CountryId",
                schema: "adsite",
                table: "Categories",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "adsite",
                table: "Cities",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdDetailPictures",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Ads",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AdDetails",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "adsite");
        }
    }
}
