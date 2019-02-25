using AdSite.Models.AdSiteDomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdSite.Data
{
    public class AdSiteDbContext : DbContext
    {
        public readonly string SCHEMA_NAME = "adsite";
        //todo: configure as generics
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdDetail> AdDetails { get; set; }
        public DbSet<AdDetailPicture> AdDetailPictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        public AdSiteDbContext(DbContextOptions<AdSiteDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(SCHEMA_NAME);
            base.OnModelCreating(builder);

            ConfigureModel.ConfigureEFModel(builder);
        }

    }
}
