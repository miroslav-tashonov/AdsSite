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
    public class CityServiceTests
    {
        private ICityRepository _cityRepository;
        private ICityService _cityService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private string SearchNameString = "cityname";
        private string SearchPostcodeString = "1000";
        private FakeDbSet<City> _cities;

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
            var _loggerRepo = Substitute.For<ILogger<CityService>>();
            var _userStore = Substitute.For<IUserStore<ApplicationUser>>();
            var _userManager = Substitute.For<UserManager<ApplicationUser>>(_userStore, null, null, null, null, null, null, null, null);


            services.AddTransient<ICityRepository, CityRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _cityRepository = new CityRepository(_memoryDbContext);
            _cityService = Substitute.For<CityService>(_cityRepository, _localizationRepo,
                _loggerRepo, _userManager);

            _cities = new FakeDbSet<City>(
                    Builder<City>.CreateListOfSize(10).TheFirst(5)
                    .With(p => p.Postcode = SearchPostcodeString)
                    .With(p => p.Name = SearchNameString)
                    .All()
                    .With(c => c.ID = Guid.NewGuid())
                    .With(cid => cid.CountryId = CountryId).Build());

            if (_memoryDbContext.Cities.Count() == 0)
            {
                _memoryDbContext.Cities.AddRange(_cities);
                _memoryDbContext.SaveChanges();
            }

        }


        [Test()]
        public void AddTest()
        {
            var count = _memoryDbContext.Cities.Count();
            var cityCreateModel = Builder<CityCreateModel>.CreateNew().Build();

            Assert.IsTrue(_cityService.Add(cityCreateModel));
            Assert.IsTrue(_memoryDbContext.Cities.Count() == count + 1);
        }

        [Test()]
        public void DeleteTest()
        {
            int count = _memoryDbContext.Cities.Count();
            var country = _memoryDbContext.Cities.FirstOrDefault();

            Assert.IsTrue(_cityService.Delete(country.ID));
            Assert.IsTrue(_memoryDbContext.Cities.Count() == count - 1);
        }

        [Test()]
        public void ExistsTest()
        {
            var city = _memoryDbContext.Cities.FirstOrDefault();

            Assert.IsTrue(_cityService.Exists(city.ID));
        }

        [Test()]
        public void GetCitiesTest()
        {
            int count = _memoryDbContext.Cities.Where(c => c.CountryId == CountryId).Count();
            Assert.IsTrue(count == _cityService.GetCities(CountryId).Count);
            Assert.IsTrue(_cityService.GetCities("name", SearchNameString, CountryId).Count > 0);
            Assert.IsTrue(_cityService.GetCities("postcode", SearchPostcodeString, CountryId).Count > 0);
        }


        [Test()]
        public void GetCityAsEditModelTest()
        {
            var city = _memoryDbContext.Cities.FirstOrDefault();
            var countryEditModel = _cityService.GetCityAsEditModel(city.ID);

            Assert.IsTrue(city.Name == countryEditModel.Name);
            Assert.IsTrue(city.Postcode == countryEditModel.Postcode);
            Assert.IsTrue(city.ID == countryEditModel.ID);
        }

        [Test()]
        public void GetCityAsViewModelTest()
        {
            var city = _memoryDbContext.Cities.FirstOrDefault();
            var countryEditModel = _cityService.GetCityAsViewModel(city.ID);

            Assert.IsTrue(city.Name == countryEditModel.Name);
            Assert.IsTrue(city.Postcode == countryEditModel.Postcode);
            Assert.IsTrue(city.ID == countryEditModel.ID);
        }

        [Test()]
        public void GetAllAsLookupTest()
        {
            var cities = _memoryDbContext.Cities.Where(c => c.CountryId == CountryId);
            var lookupsList = _cityService.GetAllAsLookup(CountryId);

            Assert.IsTrue(lookupsList.First().Id != Guid.Empty);
            Assert.IsTrue(!String.IsNullOrEmpty(lookupsList.First().Name));
        }

        [Test()]
        public void UpdateTest()
        {
            var city = _memoryDbContext.Cities.FirstOrDefault();
            var cityEditModel = Builder<CityEditModel>.CreateNew()
                .With(c => c.ID = city.ID).Build();

            Assert.IsTrue(_cityService.Update(cityEditModel));
        }
    }
}