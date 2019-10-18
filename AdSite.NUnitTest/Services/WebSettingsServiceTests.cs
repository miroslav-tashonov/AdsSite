using AdSite.Data;
using AdSite.Data.Data;
using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace AdSite.Services.Tests
{
    [TestFixture()]
    public class WebSettingsServiceTests
    {
        private IWebSettingsRepository _webSettingsRepository;
        private IWebSettingsService _webSettingsService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private FakeDbSet<WebSettings> _webSettings;

        [SetUp]
        public void Init()
        {
            var services = new ServiceCollection();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "test_db")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            _memoryDbContext = new ApplicationDbContext(options);

            //Arrange
            var _localizationRepo = Substitute.For<ILocalizationRepository>();
            var _loggerRepo = Substitute.For<ILogger<WebSettingsService>>();
            var _userStore = Substitute.For<IUserStore<ApplicationUser>>();
            var _userManager = Substitute.For<UserManager<ApplicationUser>>(_userStore, null, null, null, null, null, null, null, null);
            services.AddTransient<IWebSettingsRepository, WebSettingsRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _webSettingsRepository = new WebSettingsRepository(_memoryDbContext);
            _webSettingsService = Substitute.For<WebSettingsService>(_webSettingsRepository, _localizationRepo,
                _loggerRepo, _userManager);

            _webSettings = new FakeDbSet<WebSettings>(
                    Builder<WebSettings>.CreateListOfSize(10)
                    .All()
                    .With(c => c.ID = Guid.NewGuid())
                    .With(cid => cid.CountryId = CountryId).Build());

            if (_memoryDbContext.WebSettings.Count() == 0)
            {
                _memoryDbContext.WebSettings.AddRange(_webSettings);
                _memoryDbContext.SaveChanges();
            }

        }

        [Test()]
        public void GetWebSettingsViewModelForCountryTest()
        {
            var webSettings = _memoryDbContext.WebSettings.FirstOrDefault();
            var webSettingsViewModel = _webSettingsService.GetWebSettingsViewModelForCountry(CountryId);

            Assert.IsTrue(webSettings.Phone == webSettingsViewModel.Phone);
            Assert.IsTrue(webSettings.Email == webSettingsViewModel.Email);
            Assert.IsTrue(webSettings.FacebookSocialLink == webSettingsViewModel.FacebookSocialLink);
            Assert.IsTrue(webSettings.ID == webSettingsViewModel.ID);
        }

        [Test()]
        public void GetWebSettingsCreateModelForCountryTest()
        {
            var webSettings = _memoryDbContext.WebSettings.FirstOrDefault();
            var webSettingsViewModel = _webSettingsService.GetWebSettingsCreateModelForCountry(CountryId);

            Assert.IsTrue(webSettings.Phone == webSettingsViewModel.Phone);
            Assert.IsTrue(webSettings.Email == webSettingsViewModel.Email);
            Assert.IsTrue(webSettings.FacebookSocialLink == webSettingsViewModel.FacebookSocialLink);
        }

        [Test()]
        public void GetWebSettingsEditModelForCountryTest()
        {
            var webSettings = _memoryDbContext.WebSettings.FirstOrDefault();
            var webSettingsEditModel = _webSettingsService.GetWebSettingsEditModelForCountry(CountryId);

            Assert.IsTrue(webSettings.Phone == webSettingsEditModel.Phone);
            Assert.IsTrue(webSettings.Email == webSettingsEditModel.Email);
            Assert.IsTrue(webSettings.FacebookSocialLink == webSettingsEditModel.FacebookSocialLink);
            Assert.IsTrue(webSettings.ID == webSettingsEditModel.ID);
        }

        [Test()]
        public void WebSettingsExistForCountryTest()
        {
            Assert.IsTrue(_webSettingsService.WebSettingsExistForCountry(CountryId));
        }

        [Test()]
        public void CreateWebSettingsForCountryTest()
        {
            var entity = _memoryDbContext.WebSettings.FirstOrDefault();
            var webSettingsCreateModel = Builder<WebSettingsCreateModel>.CreateNew().Build();

            Assert.IsTrue(_webSettingsService.CreateWebSettingsForCountry(webSettingsCreateModel, CountryId));
        }

        [Test()]
        public void UpdateWebSettingsForCountryTest()
        {
            var entity = _memoryDbContext.WebSettings.FirstOrDefault();
            var webSettingsEditModel = Builder<WebSettingsEditModel>.CreateNew().Build();

            Assert.IsTrue(_webSettingsService.UpdateWebSettingsForCountry(webSettingsEditModel, CountryId));
        }
    }
}