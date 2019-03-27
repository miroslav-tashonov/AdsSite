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
    public interface ILanguageRepository : IRepository<Language>
    {

    }

    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;
        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Language entity)
        {
            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(Guid id)
        {
            var language = _context.Languages.FirstOrDefaultAsync(m => m.ID == id);
            var result = language.Result;
            if (result == null)
            {
                throw new Exception("Cannot find the entity in delete section");
            }

            _context.Languages.Remove(result);
            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Languages.Any(e => e.ID == id);
        }

        public Language Get(Guid id)
        {
            var language = _context.Languages.FirstOrDefaultAsync(m => m.ID == id);
            var result = language.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Language> GetAll(Guid countryId)
        {
            var languages = _context.Languages.Where(language => language.CountryId == countryId).ToListAsync();

            return languages.Result;
        }


        public bool Update(Language entity)
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
                    throw new Exception("Cannot save changes to db");
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
