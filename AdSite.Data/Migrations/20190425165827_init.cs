using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdSite.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "adsite");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "adsite",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "adsite",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Abbreviation = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "adsite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "adsite",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "adsite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "adsite",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "adsite",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "adsite",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "adsite",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "adsite",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "adsite",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "adsite",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "adsite",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    CountryId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Categories_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "adsite",
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false),
                    CountryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CultureId = table.Column<int>(nullable: false),
                    LanguageName = table.Column<string>(nullable: true),
                    LanguageShortName = table.Column<string>(nullable: true),
                    CountryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Languages_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleCountry",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CountryId = table.Column<Guid>(nullable: false),
                    ApplicationIdentityRoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleCountry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRoleCountry_AspNetRoles_ApplicationIdentityRoleId",
                        column: x => x.ApplicationIdentityRoleId,
                        principalSchema: "adsite",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleCountry_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "adsite",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleCountry_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebSettings",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FacebookSocialLink = table.Column<string>(nullable: true),
                    InstagramSocialLink = table.Column<string>(nullable: true),
                    TwitterSocialLink = table.Column<string>(nullable: true),
                    GooglePlusSocialLink = table.Column<string>(nullable: true),
                    VKSocialLink = table.Column<string>(nullable: true),
                    CountryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSettings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WebSettings_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ads",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    CategoryID = table.Column<Guid>(nullable: false),
                    CityID = table.Column<Guid>(nullable: false),
                    CountryID = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ads_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "adsite",
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ads_Cities_CityID",
                        column: x => x.CityID,
                        principalSchema: "adsite",
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ads_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ads_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "adsite",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LocalizationKey = table.Column<string>(nullable: false),
                    LocalizationValue = table.Column<string>(nullable: false),
                    LanguageId = table.Column<Guid>(nullable: false),
                    CountryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Localizations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "adsite",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Localizations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "adsite",
                        principalTable: "Languages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdDetails",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AdID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdDetails_Ads_AdID",
                        column: x => x.AdID,
                        principalSchema: "adsite",
                        principalTable: "Ads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    AdId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wishlists_Ads_AdId",
                        column: x => x.AdId,
                        principalSchema: "adsite",
                        principalTable: "Ads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "adsite",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdDetailPictures",
                schema: "adsite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    AdDetailID = table.Column<Guid>(nullable: false),
                    File = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdDetailPictures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AdDetailPictures_AdDetails_AdDetailID",
                        column: x => x.AdDetailID,
                        principalSchema: "adsite",
                        principalTable: "AdDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdDetailPictures_AdDetailID",
                schema: "adsite",
                table: "AdDetailPictures",
                column: "AdDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_AdDetails_AdID",
                schema: "adsite",
                table: "AdDetails",
                column: "AdID",
                unique: true);

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
                name: "IX_Ads_OwnerId",
                schema: "adsite",
                table: "Ads",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "adsite",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "adsite",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "adsite",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "adsite",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "adsite",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "adsite",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "adsite",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CountryId",
                schema: "adsite",
                table: "Categories",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                schema: "adsite",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ID_ParentId",
                schema: "adsite",
                table: "Categories",
                columns: new[] { "ID", "ParentId" },
                unique: true,
                filter: "[ParentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                schema: "adsite",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CountryId",
                schema: "adsite",
                table: "Languages",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_CountryId",
                schema: "adsite",
                table: "Localizations",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_LanguageId",
                schema: "adsite",
                table: "Localizations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleCountry_ApplicationIdentityRoleId",
                schema: "adsite",
                table: "UserRoleCountry",
                column: "ApplicationIdentityRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleCountry_ApplicationUserId",
                schema: "adsite",
                table: "UserRoleCountry",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleCountry_CountryId",
                schema: "adsite",
                table: "UserRoleCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_WebSettings_CountryId",
                schema: "adsite",
                table: "WebSettings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_AdId",
                schema: "adsite",
                table: "Wishlists",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_OwnerId",
                schema: "adsite",
                table: "Wishlists",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdDetailPictures",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Localizations",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "UserRoleCountry",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "WebSettings",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Wishlists",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AdDetails",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Ads",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "adsite");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "adsite");
        }
    }
}
