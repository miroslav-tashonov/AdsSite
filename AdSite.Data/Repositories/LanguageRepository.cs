﻿using AdSite.Models.DatabaseModels;
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
        bool Exists(int lcid, Guid countryId);
        int Count(Guid countryId);
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
            try
            {
                var language = _context.Languages.FirstOrDefaultAsync(m => m.ID == id);
                var result = language.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.Languages.Remove(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Languages.Any(e => e.ID == id);
        }
        public bool Exists(int lcid, Guid countryId)
        {
            return _context.Languages.Any(e => e.CultureId == lcid && e.CountryId == countryId);
        }



        public int Count(Guid countryId)
        {
            return _context.Languages.Count();
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
