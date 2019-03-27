using AdSite.Models;
using AdSite.Models.DatabaseModels;
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
            builder.Entity<Ad>()
                .HasOne(adDetail => adDetail.AdDetail)
                .WithOne(ad => ad.Ad)
                .HasForeignKey<AdDetail>(ad => ad.AdID);


            builder.Entity<AdDetail>()
                .HasMany(adDetailPictures => adDetailPictures.AdDetailPictures)
                .WithOne( adDetail => adDetail.AdDetail )
                .HasForeignKey(adDetail => adDetail.AdDetailID)
                .OnDelete(DeleteBehavior.Cascade);
            


            builder.Entity<City>()
                .HasOne(country => country.Country)
                .WithMany(city => city.Cities)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasOne(country => country.Country)
                .WithMany(category => category.Categories)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasMany(children => children.Children)
                .WithOne(parent => parent.Parent);

            builder.Entity<Category>()
                .HasIndex(category => new { category.ID, category.ParentId }).IsUnique();

            builder.Entity<ApplicationUser>()
                .HasMany(ads => ads.Ads)
                .WithOne(owner => owner.Owner)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<ApplicationUser>()
                .HasMany(ads => ads.Ads)
                .WithOne(owner => owner.Owner)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<UserRoleCountry>()
                .HasOne(country => country.Country)
                .WithMany(tuple => tuple.UserRoleCountry)
                .HasForeignKey(urc => urc.CountryId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserRoleCountry>()
                .HasOne(identityRole => identityRole.ApplicationIdentityRole)
                .WithMany(tuple => tuple.UserRoleCountry)
                .HasForeignKey(urc => urc.ApplicationIdentityRoleId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserRoleCountry>()
                .HasOne(identityUser => identityUser.ApplicationUser)
                .WithMany(tuple => tuple.UserRoleCountry)
                .HasForeignKey(urc => urc.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Localization>()
                .HasOne(language => language.Language)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<WebSettings>()
                .HasOne(country => country.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Language>()
                .HasOne(country => country.Country)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
