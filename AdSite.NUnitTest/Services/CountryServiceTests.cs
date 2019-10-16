using NUnit.Framework;
using AdSite.Services;
using System;
using System.Collections.Generic;
using System.Text;
using AdSite.Models.CRUDModels;
using AdSite.Data.Repositories;
using AdSite.Models.DatabaseModels;
using Microsoft.Extensions.DependencyInjection;
using FizzWare.NBuilder;
using AdSite.Data;
using NSubstitute;
using AdSite.Data.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AdSite.Services.Tests
{
    [TestFixture()]
    public class CountryServiceTests
    {
        private ICountryRepository _countryRepository;
        private ICountryService _countryService;
        private IList<Country> _countries;
        private IApplicationDbContext _memoryDbContext;

        [SetUp]
        public void Init()
        {
            var services = new ServiceCollection();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "test_db")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
            _memoryDbContext  = new ApplicationDbContext(options);

            //Arrange
            var _localizationRepo = Substitute.For<ILocalizationRepository>();
            var _loggerRepo = Substitute.For<ILogger<CountryService>>();
            

            services.AddTransient<ICountryRepository, CountryRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _countryRepository = new CountryRepository(_memoryDbContext);
            _countryService = Substitute.For<CountryService>(_countryRepository, _localizationRepo, _loggerRepo);

            FakeDbSet<Country> _countries = new FakeDbSet<Country>(
                Builder<Country>.CreateListOfSize(5).All().With(c => c.ID = Guid.NewGuid()).Build());

            _memoryDbContext.Countries.AddRange(_countries);
            _memoryDbContext.SaveChanges();

        }

        [Test()]
        public void GetCountriesTest()
        {
            var count = _memoryDbContext.Countries.Count();

            Assert.IsTrue(_countryService.GetAll().Count == count);
        }

        [Test()]
        public void AddTest()
        {
            var count = _memoryDbContext.Countries.Count();
            var countryCreateModel = Builder<CountryCreateModel>.CreateNew().With(c => c.Path = "uniquePath").Build();
            _countryService.Add(countryCreateModel);


            Assert.AreEqual(_memoryDbContext.Countries.Count(), count + 1);
        }


        [Test()]
        public void GetTest()
        {
            var country = _memoryDbContext.Countries.FirstOrDefault();
            var requestedCountry = _countryService.Get(country.ID);

            Assert.IsTrue(requestedCountry == country.ID);
        }

        [Test()]
        public void GetAllTest()
        {
            var countries = _memoryDbContext.Countries.ToList();
            var requestedCountries = _countryService.GetAll();

            Assert.IsTrue(countries.Count == requestedCountries.Count);
        }

        [Test()]
        public void GetByCountryPathTest()
        {
            var country = _memoryDbContext.Countries.FirstOrDefault();
            var requestedCountry = _countryService.GetByCountryPath(country.Path);

            Assert.IsTrue(requestedCountry != null && requestedCountry.ID != Guid.Empty);
        }

        [Test()]
        public void GetCountryAsViewModelTest()
        {
            var country = _memoryDbContext.Countries.FirstOrDefault();
            var countryViewModel = _countryService.GetCountryAsViewModel(country.ID);

            Assert.IsTrue( country.Name == countryViewModel.Name );
            Assert.IsTrue(country.Path == countryViewModel.Path);
            Assert.IsTrue(country.ID == countryViewModel.ID);
        }

        [Test()]
        public void GetCountryAsEditModelTest()
        {
            var country = _memoryDbContext.Countries.FirstOrDefault();
            var countryEditModel = _countryService.GetCountryAsEditModel(country.ID);

            Assert.IsTrue(country.Name == countryEditModel.Name);
            Assert.IsTrue(country.Path == countryEditModel.Path);
            Assert.IsTrue(country.ID == countryEditModel.ID);
        }

        [Test()]
        public void ExistTest()
        {
            var country = _memoryDbContext.Countries.FirstOrDefault();
            var countryExist = _countryService.Exist(country.ID);

            Assert.IsTrue(countryExist == true);
        }

        [Test()]
        public void UpdateTest()
        {
            var country = _memoryDbContext.Countries.FirstOrDefault();
            var countryEditModel = Builder<CountryEditModel>.CreateNew()
                .With(c => c.ID = country.ID )
                .With(c => c.Path = "someUniquePath").Build();
            
            Assert.IsTrue(_countryService.Update(countryEditModel));
        }
    }
}