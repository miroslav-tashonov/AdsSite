﻿// <auto-generated />
using AdSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdSite.Data.Migrations.AdSiteDb
{
    [DbContext(typeof(AdSiteDbContext))]
    partial class AdSiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("adsite")
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.Ad", b =>
                {
                    b.Property<int>("AdID");

                    b.Property<int>("AdDetailID");

                    b.Property<int>("CategoryID");

                    b.Property<int>("CityID");

                    b.Property<int>("CountryID");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("AdID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("CityID");

                    b.HasIndex("CountryID");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.AdDetail", b =>
                {
                    b.Property<int>("AdDetailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdID");

                    b.Property<string>("Description");

                    b.HasKey("AdDetailID");

                    b.ToTable("AdDetails");
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.AdDetailPicture", b =>
                {
                    b.Property<int>("AdDetailPictureID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdDetailID");

                    b.HasKey("AdDetailPictureID");

                    b.HasIndex("AdDetailID");

                    b.ToTable("AdDetailPictures");
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("CategoryID");

                    b.HasIndex("CountryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.Property<string>("Postcode");

                    b.HasKey("CityID");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation");

                    b.Property<string>("Name");

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.Ad", b =>
                {
                    b.HasOne("AdSite.Models.AdSiteDomainModels.AdDetail", "AdDetail")
                        .WithOne("Ad")
                        .HasForeignKey("AdSite.Models.AdSiteDomainModels.Ad", "AdID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdSite.Models.AdSiteDomainModels.Category", "Category")
                        .WithMany("Ads")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AdSite.Models.AdSiteDomainModels.City", "City")
                        .WithMany("Ads")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AdSite.Models.AdSiteDomainModels.Country", "Country")
                        .WithMany("Ads")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.AdDetailPicture", b =>
                {
                    b.HasOne("AdSite.Models.AdSiteDomainModels.AdDetail", "AdDetail")
                        .WithMany("AdDetailPictures")
                        .HasForeignKey("AdDetailID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.Category", b =>
                {
                    b.HasOne("AdSite.Models.AdSiteDomainModels.Country", "Country")
                        .WithMany("Categories")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AdSite.Models.AdSiteDomainModels.City", b =>
                {
                    b.HasOne("AdSite.Models.AdSiteDomainModels.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
