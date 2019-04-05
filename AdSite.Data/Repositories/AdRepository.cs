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
                var entity = _context.Ads.FirstOrDefaultAsync(m => m.ID == id);
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
