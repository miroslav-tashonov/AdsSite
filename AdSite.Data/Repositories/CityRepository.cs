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
    public interface ICityRepository : IRepository<City>
    {

    }

    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;
        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(City entity)
        {
            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(Guid id)
        {
            try
            {
                var city = _context.Cities.FirstOrDefaultAsync(m => m.ID == id);
                var result = city.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.Cities.Remove(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Cities.Any(e => e.ID == id);
        }

        public City Get(Guid id)
        {
            var city = _context.Cities.FirstOrDefaultAsync(m => m.ID == id);
            var result = city.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<City> GetAll(Guid countryId)
        {
            var cities = _context.Cities.Where(city => city.CountryId == countryId).ToListAsync();

            return cities.Result;
        }


        public bool Update(City entity)
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
