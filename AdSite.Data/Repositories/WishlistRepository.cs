using AdSite.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdSite.Data.Repositories
{
    public interface IWishlistRepository : IRepository<Wishlist>
    {
        List<Wishlist> GetAll(string ownerId, Guid countryId);
        bool Delete(Guid adId, string ownerId);

        bool Exists(Guid adId, string ownerId);
    }

    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _context;
        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Wishlist entity)
        {
            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(Guid adId, string currentUserId)
        {
            try
            {
                var wishlist = _context.Wishlists.FirstOrDefaultAsync(m => m.AdId == adId && m.OwnerId == currentUserId);
                var result = wishlist.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.Wishlists.Remove(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SaveChangesResult();
        }

        public bool Delete(Guid id)
        {
            try
            {
                var wishlist = _context.Wishlists.FirstOrDefaultAsync(m => m.ID == id);
                var result = wishlist.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.Wishlists.Remove(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Wishlists.Any(e => e.ID == id);
        }

        public bool Exists(Guid adId, string userId)
        {
            return _context.Wishlists.Any(e => e.AdId == adId && e.OwnerId == userId);
        }

        public Wishlist Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Wishlist> GetAll(Guid countryId)
        {
            var cities = _context.Wishlists.Where(wishlist => wishlist.CountryId == countryId).ToListAsync();

            return cities.Result;
        }

        public List<Wishlist> GetAll(string ownerId, Guid countryId)
        {
            var cities = _context.Wishlists.Where(wishlist => wishlist.CountryId == countryId && wishlist.OwnerId == ownerId).ToListAsync();

            return cities.Result;
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

        public bool Update(Wishlist entity)
        {
            throw new NotImplementedException();
        }
    }
}
