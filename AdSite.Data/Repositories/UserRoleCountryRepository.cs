using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSite.Data.Repositories
{
    public interface IUserRoleCountryRepository : IRepository<UserRoleCountry>
    {
        public bool Delete(string userId, Guid countryId);
        bool Exists(string userId, Guid countryId);
    }

    public class UserRoleCountryRepository : IUserRoleCountryRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRoleCountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(UserRoleCountry entity)
        {

            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(string userId, Guid countryId)
        {
            try
            {
                var urcs = _context.UserRoleCountries.Where(m => m.CountryId == countryId
                    && m.ApplicationUserId == userId).ToListAsync().GetAwaiter().GetResult();

                if (urcs == null)
                {
                    throw new Exception();
                }

                foreach (var urc in urcs)
                {
                    _context.UserRoleCountries.Remove(urc);
                }
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
                var urc = _context.UserRoleCountries.FirstOrDefaultAsync(m => m.ID == id);
                var result = urc.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.UserRoleCountries.Remove(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.UserRoleCountries.Any(e => e.ID == id);
        }

        public bool Exists(string userId, Guid countryId)
        {
            return _context.UserRoleCountries.Any(e => e.CountryId == countryId &&
                    e.ApplicationUserId == userId);
        }

        public UserRoleCountry Get(Guid id)
        {
            var urc = _context.UserRoleCountries.FirstOrDefaultAsync(m => m.ID == id);
            var result = urc.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<UserRoleCountry> GetAll(Guid countryId)
        {
            var urcs = _context.UserRoleCountries.ToList();

            return urcs;
        }

        public bool Update(UserRoleCountry entity)
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
