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
using Microsoft.Extensions.Logging;
using AdSite.Models;
using Microsoft.AspNetCore.Identity;
using FizzWare.NBuilder;
using System.Linq;

namespace AdSite.Services.Tests
{
    [TestFixture()]
    public class WishlistServiceTests
    {
        private IWishlistRepository _wishlistRepository;
        private IWishlistService _wishlistService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private Guid AdId = Guid.NewGuid();
        private string UserId = "OwnerId";
        private FakeDbSet<Wishlist> _wishlists;

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
            var _adsService = Substitute.For<IAdService>();
            var _loggerRepo = Substitute.For<ILogger<WishlistService>>();
            var _userStore = Substitute.For<IUserStore<ApplicationUser>>();
            var _userManager = Substitute.For<UserManager<ApplicationUser>>(_userStore, null, null, null, null, null, null, null, null);

            services.AddTransient<IWishlistRepository, WishlistRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _wishlistRepository = new WishlistRepository(_memoryDbContext);
            _wishlistService = Substitute.For<WishlistService>
                (_wishlistRepository, _localizationRepo, _adsService, _loggerRepo, _userManager);

            _wishlists = new FakeDbSet<Wishlist>(
                    Builder<Wishlist>.CreateListOfSize(10)
                    .All()
                    .With(w => w.AdId = AdId)
                    .With(w => w.OwnerId = UserId)
                    .With(c => c.ID = Guid.NewGuid())
                    .With(cid => cid.CountryId = CountryId).Build());

            if (_memoryDbContext.Wishlists.Count() == 0)
            {
                _memoryDbContext.Wishlists.AddRange(_wishlists);
                _memoryDbContext.SaveChanges();
            }

        }

        [Test()]
        public void AddTest()
        {
            string newUserId = "newuserid";
            var count = _memoryDbContext.Wishlists
                .Where(c => c.OwnerId == newUserId && c.CountryId == CountryId).Count();
            
            Assert.IsTrue(_wishlistService.Add(AdId, newUserId, CountryId));
            Assert.IsTrue(_memoryDbContext.Wishlists
                .Where(c => c.OwnerId == newUserId && c.CountryId == CountryId).Count() == count + 1);
        }

        [Test()]
        public void DeleteTest()
        {
            var count = _memoryDbContext.Wishlists.Count();

            Assert.IsTrue(_wishlistService.Delete(AdId, UserId));
            Assert.IsTrue(_memoryDbContext.Wishlists.Count() == count - 1);
        }

        [Test()]
        public void ExistsTest()
        {
            var entity = _memoryDbContext.Wishlists.FirstOrDefault();

            Assert.IsTrue(_wishlistService.Exists(entity.ID));
        }

        [Test()]
        public void IsInWishlistTest()
        {
            var entity = _memoryDbContext.Wishlists.FirstOrDefault();

            Assert.IsTrue(_wishlistService.IsInWishlist(entity.AdId, UserId));
        }

        [Test()]
        public void GetMyWishlistTest()
        {
            int count = _memoryDbContext.Wishlists
                .Where(c => c.OwnerId == UserId && c.CountryId == CountryId).Count();
            Assert.IsTrue(count == _wishlistService.GetMyWishlist(UserId, CountryId).Count);

        }
    }
}