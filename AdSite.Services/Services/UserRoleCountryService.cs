using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using AdSite.Models.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AdSite.Services
{
    public interface IUserRoleCountryService
    {
        bool Exists(string userId, Guid countryId);
        bool Delete(string userId, Guid countryId);
        bool Add(UserRoleCountryCreateModel category);
        bool Update(UserRoleCountryEditModel category);

        List<UserRoleCountryGridModel> GetAll(Guid countryId);
    }



    public class UserRoleCountryService : IUserRoleCountryService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRoleCountryRepository _repository;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_USERROLECOUNTRY_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_UserRoleCountry_Not_Found", CultureId);
        private string LOCALIZATION_GENERAL_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_General_Not_Found", CultureId);

        public UserRoleCountryService(IUserRoleCountryRepository repository, ILocalizationRepository localizationRepository,
            ILogger<UserRoleCountryService> logger, UserManager<ApplicationUser> userManager)
        {
            _localizationRepository = localizationRepository;
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public bool Add(UserRoleCountryCreateModel entity)
        {
            UserRoleCountry tuple = new UserRoleCountry
            {
                CountryId = entity.CountryId,
                ApplicationIdentityRoleId = entity.RoleId,
                ApplicationUserId = entity.ApplicationUserId,
            };

            return _repository.Add(tuple);
        }

        public bool Delete(string userId, Guid countryId)
        {
            try
            {
                return _repository.Delete(userId, countryId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception while deleting user role country  : {0} - {1} ", ex.StackTrace, ex.Message);
                throw ex;
            }
        }


        public bool Exists(string userId, Guid countryId)
        {
            return _repository.Exists(userId, countryId);
        }


        public bool Update(UserRoleCountryEditModel entity)
        {
            UserRoleCountry tuple = _repository.Get(entity.Id);
            if (tuple == null)
            {
                throw new Exception(LOCALIZATION_GENERAL_NOT_FOUND + entity.Id);
            }

            tuple.ApplicationUserId = entity.ApplicationUserId;
            tuple.ApplicationIdentityRoleId = entity.RoleId;

            return _repository.Update(tuple);
        }

        public List<UserRoleCountryGridModel> GetAll(Guid countryId)
        {
            List<UserRoleCountry> entities = _repository.GetAll(countryId);

            if (entities == null)
            {
                throw new Exception(LOCALIZATION_USERROLECOUNTRY_NOT_FOUND);
            }

            return UserRoleCountryMapper.MapToViewModel(entities);
        }


    }
}
