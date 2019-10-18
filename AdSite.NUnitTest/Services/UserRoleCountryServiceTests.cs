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
    public class UserRoleCountryServiceTests
    {
        private IUserRoleCountryRepository _urcRepository;
        private IUserRoleCountryService _urcService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private FakeDbSet<UserRoleCountry> _urcs;
        private string UserId = "UserId";

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
            var _loggerRepo = Substitute.For<ILogger<UserRoleCountryService>>();
            var _userStore = Substitute.For<IUserStore<ApplicationUser>>();
            var _userManager = Substitute.For<UserManager<ApplicationUser>>(_userStore, null, null, null, null, null, null, null, null);
            services.AddTransient<IUserRoleCountryRepository, UserRoleCountryRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _urcRepository = new UserRoleCountryRepository(_memoryDbContext);
            _urcService = Substitute.For<UserRoleCountryService>(_urcRepository, _localizationRepo,
                _loggerRepo, _userManager);

            _urcs = new FakeDbSet<UserRoleCountry>(
                    Builder<UserRoleCountry>.CreateListOfSize(10)
                    .All()
                    .With(c => c.ApplicationUserId = UserId)
                    .With(c => c.ID = Guid.NewGuid())
                    .With(cid => cid.CountryId = CountryId).Build());

            if (_memoryDbContext.UserRoleCountries.Count() == 0)
            {
                _memoryDbContext.UserRoleCountries.AddRange(_urcs);
                _memoryDbContext.SaveChanges();
            }

        }

        [Test()]
        public void AddTest()
        {
            var count = _memoryDbContext.UserRoleCountries.Count();
            var urcCreateModel = Builder<UserRoleCountryCreateModel>.CreateNew().Build();

            Assert.IsTrue(_urcService.Add(urcCreateModel));
            Assert.IsTrue(_memoryDbContext.UserRoleCountries.Count() == count + 1);
        }

        [Test()]
        public void DeleteTest()
        {
            int count = _memoryDbContext.UserRoleCountries.Count();
            var entity = _memoryDbContext.UserRoleCountries.FirstOrDefault();

            Assert.IsTrue(_urcService.Delete(UserId, CountryId));
            Assert.IsTrue(_memoryDbContext.UserRoleCountries.Count() == count - 1);
        }

        [Test()]
        public void ExistsTest()
        {
            var entity = _memoryDbContext.UserRoleCountries.FirstOrDefault();

            Assert.IsTrue(_urcService.Exists(UserId, CountryId));
        }

        [Test()]
        public void UpdateTest()
        {
            var city = _memoryDbContext.UserRoleCountries.FirstOrDefault();
            var urcEditModel = Builder<UserRoleCountryEditModel>.CreateNew()
                .With(c => c.Id = city.ID).Build();

            Assert.IsTrue(_urcService.Update(urcEditModel));
        }

        [Test()]
        public void GetAllTest()
        {
            int count = _memoryDbContext.UserRoleCountries.Where(c => c.CountryId == CountryId).Count();
            Assert.IsTrue(count == _urcService.GetAll(CountryId).Count);
        }
    }
}