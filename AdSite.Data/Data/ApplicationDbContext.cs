using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationIdentityRole, string>
    {
        public readonly string SCHEMA_NAME = "adsite";

        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdDetail> AdDetails { get; set; }
        public DbSet<AdDetailPicture> AdDetailPictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<Language> Languages{ get; set; }

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
