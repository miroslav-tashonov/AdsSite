using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdSite.Models;
using AdSite.Models.AdSiteDomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public readonly string SCHEMA_NAME = "users";
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
        }
    }
}
