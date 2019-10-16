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
    public interface IApplicationDbContext
    {
        DbSet<Ad> Ads { get; set; }
        DbSet<AdDetail> AdDetails { get; set; }
        DbSet<AdDetailPicture> AdDetailPictures { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Localization> Localizations { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Wishlist> Wishlists { get; set; }
        DbSet<WebSettings> WebSettings { get; set; }
        DbSet<UserRoleCountry> UserRoleCountries { get; set; }

        int SaveChanges();
        EntityEntry Add(object entity);
        EntityEntry Update(object entity);
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        

    }

}
