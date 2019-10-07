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
    public interface ICountryRepository : ICountryRepository<Country>
    {
        Country Get(Guid countryId);
        List<Country> GetAll();
        List<Country> GetByCountryName(string searchString);

        List<Country> GetByCountryPath(string searchString);

        Country GetCountryByPath(string searchString);
    }

    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Country entity)
        {
            
            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(Guid id)
        {
            try
            {
                var country = _context.Countries.FirstOrDefaultAsync(m => m.ID == id);
                var result = country.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.Countries.Remove(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Countries.Any(e => e.ID == id);
        }

        public Country Get(Guid id)
        {
            var country = _context.Countries.FirstOrDefaultAsync(m => m.ID == id);
            var result = country.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Country> GetAll()
        {
            var cities = _context.Countries.ToListAsync();

            return cities.Result;
        }

        public List<Country> GetByCountryName(string searchString)
        {
            var countries = _context.Countries.Where(country => country.Name.Contains(searchString)).ToListAsync();

            return countries.Result;
        }

        public List<Country> GetByCountryPath(string searchString)
        {
            var countries = _context.Countries.Where(country => country.Path.Contains(searchString)).ToListAsync();

            return countries.Result;
        }

        public Country GetCountryByPath(string searchString)
        {
            Country country = new Country();
            if (_context.Countries.Any(country => country.Path.ToLower() == searchString.ToLower()))
                country = _context.Countries.Where(country => country.Path.ToLower() == searchString.ToLower()).First();

            return country;
        }

        public bool Update(Country entity)
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
