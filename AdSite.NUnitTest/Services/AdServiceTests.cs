using NUnit.Framework;
using AdSite.Services;
using System;
using System.Collections.Generic;
using System.Text;
using AdSite.Data.Repositories;
using AdSite.Data;
using AdSite.Models.DatabaseModels;
using AdSite.Data.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using AdSite.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using FizzWare.NBuilder;
using System.Linq;

namespace AdSite.Services.Tests
{
    [TestFixture()]
    public class AdServiceTests
    {
        private IAdRepository _adRepository;
        private IAdService _adService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private FakeDbSet<Ad> _ads;

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
            var _categoryRepo = Substitute.For<ICategoryRepository>();
            var _cityRepository = Substitute.For<ICityRepository>();
            var _wishlistRepo = Substitute.For<IWishlistRepository>();
            var _loggerRepo = Substitute.For<ILogger<AdService>>();

            services.AddTransient<IAdRepository, AdRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _adRepository = new AdRepository(_memoryDbContext);
            _adService = Substitute.For<AdService>
                (_adRepository, _localizationRepo, _categoryRepo,
                _cityRepository, _wishlistRepo, _loggerRepo);

            var adId = Guid.NewGuid();
            var adDetail = Builder<AdDetail>.CreateNew()
                .With(ad=> ad.ID = Guid.NewGuid())
                .With(c => c.AdID = adId).Build();
            _ads = new FakeDbSet<Ad>(
                    Builder<Ad>.CreateListOfSize(10)
                    .TheFirst(1)
                    .With(a => a.AdDetail = adDetail)
                    .All()
                    .With(c => c.ID = Guid.NewGuid())
                    .With(cid => cid.CountryID = CountryId).Build());

            if (_memoryDbContext.Ads.Count() == 0)
            {
                _memoryDbContext.Ads.AddRange(_ads);
                _memoryDbContext.SaveChanges();
            }

        }

        [Test()]
        public void AddTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DeleteTest()
        {
            var count = _memoryDbContext.Ads.Count();
            var ad = _memoryDbContext.Ads.FirstOrDefault();

            Assert.IsTrue(_adService.Delete(ad.ID));
            Assert.IsTrue(_memoryDbContext.Ads.Count() == count - 1);
        }

        [Test()]
        public void ExistsTest()
        {
            var entity = _memoryDbContext.Ads.FirstOrDefault();

            Assert.IsTrue(_adService.Exists(entity.ID));
        }

        [Test()]
        public void CountTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetAdsTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetAdAsEditModelTest()
        {
            var ad = _memoryDbContext.Ads.Where(a => a.AdDetail != null).FirstOrDefault();
            var adEditModel = _adService.GetAdAsEditModel(ad.ID);

            Assert.IsTrue(ad.Name == adEditModel.Name);
            Assert.IsTrue(ad.ID == adEditModel.ID);
        }

        [Test()]
        public void GetAdAsViewModelTest()
        {
            var ad = _memoryDbContext.Ads.Where(a => a.AdDetail != null).FirstOrDefault();
            var adViewModel = _adService.GetAdAsViewModel(ad.ID);

            Assert.IsTrue(ad.Name == adViewModel.Name);
            Assert.IsTrue(ad.ID == adViewModel.ID);
        }

        [Test()]
        public void GetAdAsAdWishlistGridModelTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetAdGridModelTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetPageForAdGridTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetPageForMyAdsGridTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetPageForAdGridByFilterTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void UpdateTest()
        {
            Assert.Fail();
        }
    }
}