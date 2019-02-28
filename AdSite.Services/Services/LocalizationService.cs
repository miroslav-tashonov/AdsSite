using AdSite.Data.Repositories;
using AdSite.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdSite.Services.LocalizationService
{
    public interface ILocalizationService
    {
        Localization Get(Guid localizationId);

         
    }



    public class LocalizationService : ILocalizationService
    {
        private readonly IRepository<Localization> _repository;
        public LocalizationService(IRepository<Localization> repository)
        {
            _repository = repository;
        }

        public Localization Get(Guid localizationId = new Guid())
        {
            return _repository.Get(localizationId);
        }
    }
}
