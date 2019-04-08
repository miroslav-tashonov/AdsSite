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
            throw new NotImplementedException();
        }

        public bool Exists(Guid id)
        {
            return _context.Countries.Any(e => e.ID == id);
        }

        public Country Get(Guid id)
        {
            return new Country()
            {
                ID = new Guid("9B0CBFD6-0070-4285-B353-F13189BD2291")
            };
        }

        public List<Country> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Country entity)
        {
            throw new NotImplementedException();
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
