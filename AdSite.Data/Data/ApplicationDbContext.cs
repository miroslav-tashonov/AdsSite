using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdSite.Data
{
    public class ApplicationDbContext : 
        IdentityDbContext<ApplicationUser, ApplicationIdentityRole, string>, IApplicationDbContext
    {
        public readonly string SCHEMA_NAME = "adsite";

        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<AdDetail> AdDetails { get; set; }
        public virtual DbSet<AdDetailPicture> AdDetailPictures { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Localization> Localizations { get; set; }
        public virtual DbSet<Language> Languages{ get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<WebSettings> WebSettings { get; set; }
        public virtual DbSet<UserRoleCountry> UserRoleCountries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(SCHEMA_NAME);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            ConfigureModel.ConfigureEFModel(builder);
        }
    }
}
