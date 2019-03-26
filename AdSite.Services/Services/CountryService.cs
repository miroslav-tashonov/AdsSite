using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdSite.Services
{
    public interface ICountryService
    {
        Guid Get();
    }



    public class CountryService : ICountryService
    {
        
        private readonly ICountryRepository _repository;
        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        //todo Get country from context 
        public Guid Get()
        {
            return _repository.Get(Guid.Empty).ID;
        }
    }
}
