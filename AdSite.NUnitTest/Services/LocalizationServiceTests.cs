using AdSite.Data;
using AdSite.Data.Data;
using AdSite.Data.Repositories;
using AdSite.Models.CRUDModels;
using AdSite.Models.DatabaseModels;
using FizzWare.NBuilder;
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
    public class LocalizationServiceTests
    {
        private ILocalizationRepository _localizationRepository;
        private ILocalizationService _localizationService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private Guid LanguageId = Guid.NewGuid();
        private FakeDbSet<Localization> _localization;
        private int CultureId = 1034;

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
            var _languageRepo = Substitute.For<ILanguageRepository>();
            var _loggerRepo = Substitute.For<ILogger<LocalizationService>>();

            services.AddTransient<ILocalizationRepository, LocalizationRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _localizationRepository = new LocalizationRepository(_memoryDbContext);
            _localizationService = Substitute.For<LocalizationService>(_localizationRepository, _languageRepo,
                _loggerRepo);

            var language = Builder<Language>.CreateNew()
                .With(c => c.CultureId = CultureId)
                .With(c => c.ID = LanguageId)
                .With(c => c.CountryId = CountryId)
                .Build();
            _languageRepo.Get(LanguageId).Returns(language);

            _localization = new FakeDbSet<Localization>(
                    Builder<Localization>.CreateListOfSize(10)
                    .All()
                    .With(c => c.ID = Guid.NewGuid())
                    .With(c => c.LanguageId = LanguageId)
                    .With(c => c.Language = language)
                    .With(cid => cid.CountryId = CountryId).Build());

            if (_memoryDbContext.Localizations.Count() == 0)
            {
                _memoryDbContext.Localizations.AddRange(_localization);
                _memoryDbContext.SaveChanges();
            }

        }

        [Test()]
        public void AddTest()
        {
            var count = _memoryDbContext.Localizations.Count();
            var localizationCreateModel = Builder<LocalizationCreateModel>.CreateNew()
                .With(c => c.LanguageId = LanguageId)
                .Build();

            Assert.IsTrue(_localizationService.Add(localizationCreateModel));
            Assert.IsTrue(_memoryDbContext.Localizations.Count() == count + 1);
        }

        [Test()]
        public void DeleteTest()
        {
            int count = _memoryDbContext.Localizations.Count();
            var country = _memoryDbContext.Localizations.FirstOrDefault();

            Assert.IsTrue(_localizationService.Delete(country.ID));
            Assert.IsTrue(_memoryDbContext.Localizations.Count() == count - 1);
        }

        [Test()]
        public void ExistsTest()
        {
            var entity = _memoryDbContext.Localizations.FirstOrDefault();

            Assert.IsTrue(_localizationService.Exists(entity.ID));
        }

        [Test()]
        public void GetLocalizationAsEditModelTest()
        {
            var entity = _memoryDbContext.Localizations.FirstOrDefault();
            var localizationEditModel = _localizationService.GetLocalizationAsEditModel(entity.ID);

            Assert.IsTrue(entity.LocalizationKey == localizationEditModel.LocalizationKey);
            Assert.IsTrue(entity.LocalizationValue == localizationEditModel.LocalizationValue);
        }

        [Test()]
        public void GetLocalizationAsViewModelTest()
        {
            var entity = _memoryDbContext.Localizations.FirstOrDefault();
            var localizationViewModel = _localizationService.GetLocalizationAsViewModel(entity.ID);

            Assert.IsTrue(entity.LocalizationKey == localizationViewModel.LocalizationKey);
            Assert.IsTrue(entity.LocalizationValue == localizationViewModel.LocalizationValue);
        }

        [Test()]
        public void GetAllTest()
        {
            int count = _memoryDbContext.Localizations.Where(c => c.CountryId == CountryId).Count();
            Assert.IsTrue(count == _localizationService.GetAll(String.Empty, String.Empty, CountryId).Count);
            Assert.IsTrue(_localizationService.GetAll("localizationkey", String.Empty, CountryId).Count > 0);
            Assert.IsTrue(_localizationService.GetAll("localizationvalue", String.Empty, CountryId).Count > 0);
        }

        [Test()]
        public void GetByKeyTest()
        {
            var localization = _memoryDbContext.Localizations.Where(c => c.CountryId == CountryId)
                .FirstOrDefault();

            string returnedValue = _localizationService.GetByKey(localization.LocalizationKey, CultureId);
            Assert.IsTrue(!String.IsNullOrEmpty(returnedValue));
        }

        [Test()]
        public void UpdateTest()
        {
            var count = _memoryDbContext.Localizations.Count();
            var entity = _memoryDbContext.Localizations.FirstOrDefault();
            var entityEditModel = Builder<LocalizationEditModel>.CreateNew()
                .With(c => c.LanguageId = LanguageId)
                .With(c => c.Id = entity.ID).Build();

            Assert.IsTrue(_localizationService.Update(entityEditModel));
            Assert.IsTrue(_memoryDbContext.Localizations.Count() == count);
        }
    }
}