using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using AdSite.Models.Mappers;

namespace AdSite.Services
{
    public interface IWebSettingsService
    {
        bool CreateWebSettingsForCountry(WebSettingsCreateModel category, Guid countryId);
        bool UpdateWebSettingsForCountry(WebSettingsEditModel category, Guid countryId);
        WebSettingsViewModel GetWebSettingsForCountry(Guid countryId);
        bool WebSettingsExistForCountry(Guid countryId);
    }



    public class WebSettingsService : IWebSettingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebSettingsRepository _repository;
        private readonly ILogger _logger;
        public WebSettingsService(IWebSettingsRepository repository, ILogger<WebSettingsService> logger, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public WebSettingsViewModel GetWebSettingsForCountry(Guid countryId)
        {
            var entity = _repository.GetWebSettingsForCountry(countryId);
            if (entity == null)
                throw new Exception("Web settings entity couldnt be found");

            return WebSettingsMapper.MapToWebSettingsViewModel(entity);
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
            webSettings.FacebookSocialLink = entity.FacebookSocialLink;
            webSettings.TwitterSocialLink = entity.TwitterSocialLink;
            webSettings.VKSocialLink = entity.VKSocialLink;
            webSettings.InstagramSocialLink = entity.InstagramSocialLink;
            webSettings.GooglePlusSocialLink = entity.GooglePlusSocialLink;

            return _repository.UpdateWebSettingsForCountry(webSettings);
        }

    }
}
