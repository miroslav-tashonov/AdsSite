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

        bool Add();
        bool Exist(Guid Id);
    }



    public class CountryService : ICountryService
    {
        
        private readonly ICountryRepository _repository;
        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public bool Add()
        {
            var country = new Country()
            {
                ID = new Guid("9B0CBFD6-0070-4285-B353-F13189BD2291"),
                Name = "Engleska",
                Abbreviation = "en",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                CreatedBy = "Tashonov",
                ModifiedBy = "Tashonov"
            };

            return _repository.Add(country);
        }

        //todo Get country from context 
        public Guid Get()
        {
            return _repository.Get(Guid.Empty).ID;
        }

        public bool Exist(Guid ID)
        {
            return _repository.Exists(ID);
        }
    }
}
