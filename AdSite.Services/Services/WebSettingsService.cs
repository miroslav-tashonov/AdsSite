﻿using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using AdSite.Models.Mappers;
using System.Threading;

namespace AdSite.Services
{
    public interface IWebSettingsService
    {
        bool CreateWebSettingsForCountry(WebSettingsCreateModel category, Guid countryId);
        bool UpdateWebSettingsForCountry(WebSettingsEditModel category, Guid countryId);
        WebSettingsViewModel GetWebSettingsViewModelForCountry(Guid countryId);
        WebSettingsCreateModel GetWebSettingsCreateModelForCountry(Guid countryId);
        WebSettingsEditModel GetWebSettingsEditModelForCountry(Guid countryId);
        bool WebSettingsExistForCountry(Guid countryId);
    }



    public class WebSettingsService : IWebSettingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILocalizationRepository _localizationRepository;
        private readonly IWebSettingsRepository _repository;
        private readonly ILogger _logger;

        private readonly int CultureId = Thread.CurrentThread.CurrentCulture.LCID;
        private string LOCALIZATION_WEBSETTINGS_NOT_FOUND => _localizationRepository.GetLocalizationValue("Localization_WebSettings_Not_Found", CultureId);

        public WebSettingsService(IWebSettingsRepository repository, ILocalizationRepository localizationRepository, ILogger<WebSettingsService> logger, UserManager<ApplicationUser> userManager)
        {
            _localizationRepository = localizationRepository;
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public WebSettingsViewModel GetWebSettingsViewModelForCountry(Guid countryId)
        {
            var entity = _repository.GetWebSettingsForCountry(countryId);
            if (entity == null)
                entity = new WebSettings();

            return WebSettingsMapper.MapToWebSettingsViewModel(entity);
        }

        public WebSettingsCreateModel GetWebSettingsCreateModelForCountry(Guid countryId)
        {
            var entity = _repository.GetWebSettingsForCountry(countryId);
            if (entity == null)
                entity = new WebSettings();

            return WebSettingsMapper.MapToWebSettingsCreateModel(entity);
        }

        public WebSettingsEditModel GetWebSettingsEditModelForCountry(Guid countryId)
        {
            var entity = _repository.GetWebSettingsForCountry(countryId);
            if (entity == null)
                entity = new WebSettings();

            return WebSettingsMapper.MapToWebSettingsEditModel(entity);
        }

        public bool WebSettingsExistForCountry(Guid countryId)
        {
            return _repository.WebSettingsExistForCountry(countryId);
        }

        public bool CreateWebSettingsForCountry(WebSettingsCreateModel entity, Guid countryId)
        {
            WebSettings webSettings = new WebSettings()
            {
                Phone = entity.Phone,
                Email = entity.Email,
                FacebookSocialLink = entity.FacebookSocialLink,
                TwitterSocialLink = entity.TwitterSocialLink,
                VKSocialLink = entity.VKSocialLink,
                InstagramSocialLink = entity.InstagramSocialLink,
                GooglePlusSocialLink = entity.GooglePlusSocialLink,
                CountryId = countryId
            };

            return _repository.CreateWebSettingsForCountry(webSettings);
        }

        public bool UpdateWebSettingsForCountry(WebSettingsEditModel entity, Guid countryId)
        {
            WebSettings webSettings = _repository.GetWebSettingsForCountry(countryId);
            if (webSettings == null)
            {
                throw new Exception();
            }

            webSettings.Phone = entity.Phone;
            webSettings.Email = entity.Email;
            webSettings.Address = entity.Address;
            webSettings.LogoImagePath = entity.LogoImagePath;
            webSettings.Title = entity.Title;
            webSettings.FacebookSocialLink = entity.FacebookSocialLink;
            webSettings.TwitterSocialLink = entity.TwitterSocialLink;
            webSettings.VKSocialLink = entity.VKSocialLink;
            webSettings.InstagramSocialLink = entity.InstagramSocialLink;
            webSettings.GooglePlusSocialLink = entity.GooglePlusSocialLink;

            return _repository.UpdateWebSettingsForCountry(webSettings);
        }

    }
}
