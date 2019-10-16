using AdSite.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSite.Data.Repositories
{
    public interface ILocalizationRepository : IRepository<Localization>
    {
        string GetLocalizationValue(string key, int lcid);

        List<Localization> GetByLocalizationKey(string searchString, Guid countryId);

        List<Localization> GetByLocalizationValue(string searchString, Guid countryId);
    }

    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly IApplicationDbContext _context;
        public LocalizationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Localization entity)
        {
            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(Guid id)
        {
            var localization = Get(id);
            _context.Localizations.Remove(localization);
            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Localizations.Any(e => e.ID == id);
        }

        public Localization Get(Guid id)
        {
            var localization = _context.Localizations.Where(m => m.ID == id).Include(l => l.Language).FirstOrDefaultAsync();
            var result = localization.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Localization> GetAll(Guid countryId)
        {
            var localizations = _context.Localizations.Where(c => c.Language.CountryId == countryId).Include(l => l.Language).ToListAsync();
            return localizations.Result;
        }

        public List<Localization> GetByLocalizationKey(string searchString, Guid countryId)
        {
            var localizations = _context.Localizations.Where(c => c.Language.CountryId == countryId && c.LocalizationKey.Contains(searchString))
                                                      .Include(l => l.Language)
                                                      .ToListAsync();

            return localizations.Result;
        }

        public List<Localization> GetByLocalizationValue(string searchString, Guid countryId)
        {
            var localizations = _context.Localizations.Where(c => c.Language.CountryId == countryId && c.LocalizationValue.Contains(searchString))
                                                      .Include(l => l.Language)
                                                      .ToListAsync();

            return localizations.Result;
        }

        /// <summary>
        ///     Returns Localization Value based on localization key and language culture ID
        /// </summary>
        public string GetLocalizationValue(string key, int cultureId)
        {
            var localization = _context.Localizations.Where
                (x => x.Language.CultureId == cultureId && 
                x.LocalizationKey == key);

            string returnValue = localization.Count() > 0 ? 
                localization.FirstOrDefault().LocalizationValue : key;

            return returnValue;
        }

        public bool Update(Localization entity)
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
