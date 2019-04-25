﻿using AdSite.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdSite.Data.Repositories
{
    public interface IAdRepository : IRepository<Ad>
    {
        List<Ad> GetByAdName(string searchString, Guid countryId);
        List<Ad> GetAdPage(List<Ad> source, int pageIndex, int pageSize);
        Ad GetAdWithDetails(Guid id);
        List<Ad> GetAdGrid(Guid countryId);
        List<Ad> GetAdGrid(string searchString, Guid countryId);
        List<Ad> GetMyAdsGrid(string searchString, string ownerIdentifier, Guid countryId);
        List<Ad> GetAdGridByCategory(List<Guid> categoryIds, Guid countryId);
        List<Ad> GetAdGridByCategory(string searchString, List<Guid> categoryId, Guid countryId);
        List<Ad> OrderAdsByColumn(List<Ad> entities, string sortColumn);
        int Count();
    }

    public class AdRepository : IAdRepository
    {
        private readonly ApplicationDbContext _context;
        public AdRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Ad entity)
        {
            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(Guid id)
        {
            try
            {
                var entity = _context.Ads.FirstOrDefaultAsync(m => m.ID == id );
                var result = entity.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.Ads.Remove(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Ads.Any(e => e.ID == id);
        }

        public int Count()
        {
            return _context.Ads.Count();
        }

        public Ad Get(Guid id)
        {
            var ad = _context.Ads.FirstOrDefaultAsync(m => m.ID == id);
            var result = ad.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public Ad GetAdWithDetails(Guid id)
        {
            var ad = _context.Ads.Where(q => q.ID == id)
                ?.Include(c => c.City)
                ?.Include(c => c.Category)
                ?.Include(o => o.Owner)
                ?.Include(i => i.AdDetail)
                ?.ThenInclude(t => t.AdDetailPictures)
                ?.FirstOrDefaultAsync();

            var result = ad.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Ad> GetAdGrid(Guid countryId)
        {
            var ads = _context.Ads.Include(c => c.City)
                .Where(c => c.CountryID == countryId)
                .Include(c => c.Category)
                .Include(o => o.Owner)
                .Include(i => i.AdDetail)
                ?.ThenInclude(t => t.AdDetailPictures)
                .ToListAsync();

            var result = ads.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Ad> GetAdGrid(string searchString, Guid countryId)
        {
            var ads = _context.Ads.Include(c => c.City)
                .Where(c => c.CountryID == countryId && c.Name.Contains(searchString))
                .Include(c => c.Category)
                .Include(o => o.Owner)
                .Include(i => i.AdDetail)
                ?.ThenInclude(t => t.AdDetailPictures)
                .ToListAsync();

            var result = ads.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Ad> GetAdGridByCategory(List<Guid> categoryIds, Guid countryId)
        {
            var ads = _context.Ads.Include(c => c.Category)
                .Where(c => categoryIds.Contains(c.CategoryID) && c.CountryID == countryId)
                .Include(c => c.City)
                .Include(o => o.Owner)
                .Include(i => i.AdDetail)
                ?.ThenInclude(t => t.AdDetailPictures)
                .ToListAsync();

            var result = ads.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Ad> GetMyAdsGrid(string searchString, string ownerIdentifier, Guid countryId)
        {
            var ads = _context.Ads.Include(c => c.Category)
                .Where(c => c.OwnerId.Equals(ownerIdentifier) && c.CountryID == countryId && c.Name.Contains(searchString))
                .Include(c => c.City)
                .Include(o => o.Owner)
                .Include(i => i.AdDetail)
                ?.ThenInclude(t => t.AdDetailPictures)
                .ToListAsync();

            var result = ads.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Ad> GetAdGridByCategory(string searchString, List<Guid> categoryIds, Guid countryId)
        {
            var ads = _context.Ads.Include(c => c.Category)
                .Where(c => categoryIds.Contains(c.CategoryID) && c.CountryID == countryId && c.Name.Contains(searchString))
                .Include(c => c.City)
                .Include(o => o.Owner)
                .Include(i => i.AdDetail)
                ?.ThenInclude(t => t.AdDetailPictures)
                .ToListAsync();

            var result = ads.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Ad> OrderAdsByColumn(List<Ad> entities, string sortColumn)
        {
            switch (sortColumn.ToLower())
            {
                case "priciest":
                    entities = entities.OrderByDescending(o => o.Price).ToList();
                    break;
                case "cheapest":
                    entities = entities.OrderBy(o => o.Price).ToList();
                    break;
                case "latest":
                    entities = entities.OrderByDescending(o => o.ModifiedAt).ToList();
                    break;
                default:
                    break;
            }

            return entities;
        }


        public List<Ad> GetAdPage(List<Ad> source, int pageIndex, int pageSize)
        {
            var ads = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            if (ads == null)
            {
                throw new Exception();
            }

            return ads;
        }


        public List<Ad> GetAll(Guid countryId)
        {
            var ads = _context.Ads.Where(ad => ad.CountryID == countryId).ToListAsync();

            return ads.Result;
        }

        public List<Ad> GetByAdName(string searchString, Guid countryId)
        {
            var ads = _context.Ads.Where(ad => ad.CountryID == countryId && ad.Name.Contains(searchString)).ToListAsync();

            return ads.Result;
        }



        public bool Update(Ad entity)
        {
            _context.Update(entity);
            return SaveChangesResult();
        }

        public bool SaveChangesResult()
        {
            try
            {
                var result = _context.SaveChangesAsync();
                if (result.Result == 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

    }
}