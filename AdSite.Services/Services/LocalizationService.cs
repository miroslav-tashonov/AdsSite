using AdSite.Data.Repositories;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdSite.Services
{
    public interface ILocalizationService
    {
        Localization Get(Guid localizationId);
        string GetByKey(string localizationKey, int cultureId);
        List<Localization> GetAll();
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(Localization localization);
        bool Update(Localization localization);
    }



    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationRepository _repository;
        public LocalizationService(ILocalizationRepository repository)
        {
            _repository = repository;
        }

        public bool Add(Localization localization)
        {
            return _repository.Update(localization);            
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }

        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public Localization Get(Guid localizationId = new Guid())
        {
            return _repository.Get(localizationId);
        }

        public List<Localization> GetAll()
        {
            return _repository.GetAll();
        }

        public string GetByKey(string localizationKey, int cultureId)
        {
            return _repository.GetLocalizationValue(localizationKey, cultureId);
        }

        public bool Update(Localization localization)
        {
            return _repository.Update(localization);
        }
    }
}
