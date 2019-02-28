using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Data.Repositories
{
    public class LocalizationRepository : IRepository<Localization>
    {
        private readonly ApplicationDbContext _context;
        public LocalizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Localization entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid? id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Localization entity)
        {
            throw new NotImplementedException();
        }

        public Localization Get(Guid id)
        {
            return _context.Localizations.Find(id);
        }

        public List<Localization> GetAll()
        {
            throw new NotImplementedException();
        }

        public Localization Update(Localization entity)
        {
            throw new NotImplementedException();
        }
    }
}
