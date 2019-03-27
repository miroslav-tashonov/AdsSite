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
    }

    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocalizationRepository(ApplicationDbContext context)
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
            var localization = _context.Localizations.FirstOrDefaultAsync(m => m.ID == id);
            var result = localization.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Localization> GetAll(Guid countryId)
        {
            var localizations = _context.Localizations.Where(c => c.Language.CountryId == countryId).ToListAsync();
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
                localization.FirstOrDefault().LocalizationValue : String.Empty;

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
                throw new Exception();
            }
            return true;
        }
    }
}
