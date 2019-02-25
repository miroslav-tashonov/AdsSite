using AdSite.Models.AdSiteDomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Data
{
    public class ConfigureModel
    {

        public static void ConfigureEFModel(ModelBuilder builder)
        {
            builder.Entity<Ad>()
                .HasOne(category => category.Category)
                .WithMany(ads => ads.Ads)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Ad>()
                .HasOne(city => city.City)
                .WithMany(ads => ads.Ads)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Ad>()
                .HasOne(country => country.Country)
                .WithMany(ads => ads.Ads)
                .OnDelete(DeleteBehavior.Restrict);
            

            builder.Entity<AdDetail>()
                .HasMany(adDetailPictures => adDetailPictures.AdDetailPictures)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AdDetail>()
                .HasOne(ad => ad.Ad)
                .WithOne(adDetail => adDetail.AdDetail)
                .HasForeignKey<Ad>( ad => ad.AdID );

            builder.Entity<AdDetailPicture>()
                .HasOne(adDetail => adDetail.AdDetail)
                .WithMany(adDetailPictures => adDetailPictures.AdDetailPictures);


            builder.Entity<City>()
                .HasOne(country => country.Country)
                .WithMany(city => city.Cities)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasOne(country => country.Country)
                .WithMany(category => category.Categories);


            /*builder.Entity<Ad>().ToTable("Ad");
            builder.Entity<AdDetail>().ToTable("AdDetail");
            builder.Entity<AdDetailPicture>().ToTable("AdDetailPicture");
            builder.Entity<City>().ToTable("City");
            builder.Entity<Country>().ToTable("Country");
            builder.Entity<Category>().ToTable("Category");*/
        }
    }
}
