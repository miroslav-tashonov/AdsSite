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
    public interface IWebSettingsRepository
    {
        WebSettings GetWebSettingsForCountry(Guid countryId);
        bool UpdateWebSettingsForCountry(WebSettings entity);
        bool WebSettingsExistForCountry(Guid countryId);
        bool CreateWebSettingsForCountry(WebSettings entity);
    }

    public class WebSettingsRepository : IWebSettingsRepository
    {
        private readonly IApplicationDbContext _context;
        public WebSettingsRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public WebSettings GetWebSettingsForCountry(Guid countryId)
        {
            var webSettings = _context.WebSettings.FirstOrDefaultAsync(m => m.CountryId == countryId);
            var result = webSettings.Result;
            return result;
        }
        public bool WebSettingsExistForCountry(Guid countryId)
        {
            var webSettings = _context.WebSettings.AnyAsync(m => m.CountryId == countryId);
            var result = webSettings.Result;
            return result;
        }


        public bool UpdateWebSettingsForCountry(WebSettings entity)
        {
            _context.Update(entity);
            return SaveChangesResult();
        }

        public bool CreateWebSettingsForCountry(WebSettings entity)
        {
            _context.Add(entity);
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
