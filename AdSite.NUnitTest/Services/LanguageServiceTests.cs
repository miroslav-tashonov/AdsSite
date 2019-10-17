using NUnit.Framework;
using AdSite.Services;
using System;
using System.Collections.Generic;
using System.Text;
using AdSite.Data.Repositories;
using AdSite.Data;
using AdSite.Data.Data;
using AdSite.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Microsoft.Extensions.Logging;
using AdSite.Models;
using Microsoft.AspNetCore.Identity;
using FizzWare.NBuilder;
using System.Linq;
using AdSite.Models.CRUDModels;

namespace AdSite.Services.Tests
{
    [TestFixture()]
    public class LanguageServiceTests
    {
        private ILanguageRepository _languageRepository;
        private ILanguageService _languageService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private FakeDbSet<Language> _languages;

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
            var _loggerRepo = Substitute.For<ILogger<LanguageService>>();
            var _userStore = Substitute.For<IUserStore<ApplicationUser>>();
            var _userManager = Substitute.For<UserManager<ApplicationUser>>(_userStore, null, null, null, null, null, null, null, null);


            services.AddTransient<ILanguageRepository, LanguageRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _languageRepository = new LanguageRepository(_memoryDbContext);
            _languageService = Substitute.For<LanguageService>(_languageRepository, _localizationRepo,
                _loggerRepo, _userManager);

            Random random = new Random();
            _languages = new FakeDbSet<Language>(
                    Builder<Language>.CreateListOfSize(10)
                    .All()
                    .With(c => c.ID = Guid.NewGuid())
                    .With(lid => lid.CultureId = random.Next(1,1000))
                    .With(cid => cid.CountryId = CountryId).Build());

            if (_memoryDbContext.Languages.Count() == 0)
            {
                _memoryDbContext.Languages.AddRange(_languages);
                _memoryDbContext.SaveChanges();
            }

        }

        [Test()]
        public void AddTest()
        {
            var count = _memoryDbContext.Languages.Count();
            var entityCreateModel = Builder<LanguageCreateModel>.CreateNew()
                .With(lcid => lcid.CultureId = "1034")
                .Build();

            Assert.IsTrue(_languageService.Add(entityCreateModel));
            Assert.IsTrue(_memoryDbContext.Localizations.Count() == count + 1);
        }

        [Test()]
        public void DeleteTest()
        {
            int count = _memoryDbContext.Languages.Count();
            var language = _memoryDbContext.Languages.FirstOrDefault();

            Assert.IsTrue(_languageService.Delete(language.ID, CountryId));
            Assert.IsTrue(_memoryDbContext.Languages.Count() == count - 1);
        }

        [Test()]
        public void ExistsTest()
        {
            var language = _memoryDbContext.Languages.FirstOrDefault();

            Assert.IsTrue(_languageService.Exists(language.ID));
            Assert.IsTrue(_languageService.Exists(language.CultureId, CountryId));
        }


        [Test()]
        public void GetAllTest()
        {
            int count = _memoryDbContext.Languages.Where(c => c.CountryId == CountryId).Count();
            Assert.IsTrue(count == _languageService.GetAll(CountryId).Count) ;
            Assert.IsTrue(_languageService.GetAll("name", String.Empty, CountryId).Count == count);
            Assert.IsTrue(_languageService.GetAll("postcode", String.Empty, CountryId).Count == count);

        }

        [Test()]
        public void GetByCultureIdTest()
        {
            var language = _memoryDbContext.Languages.FirstOrDefault();
            var entity = _languageService.GetByCultureId(language.CultureId, CountryId);

            Assert.IsTrue(language.CultureId == entity.CultureId) ;
            Assert.IsTrue(language.LanguageName == entity.LanguageName);
            Assert.IsTrue(language.ID == entity.ID);
        }

        [Test()]
        public void GetAllAsLookupTest()
        {
            var entities = _memoryDbContext.Languages.Where(c => c.CountryId == CountryId);
            var lookupsList = _languageService.GetAllAsLookup(CountryId);

            Assert.IsTrue(lookupsList.First().Id != Guid.Empty);
            Assert.IsTrue(!String.IsNullOrEmpty(lookupsList.First().Name));
        }
    }
}