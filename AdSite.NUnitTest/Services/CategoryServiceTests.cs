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
    public class CategoryServiceTests
    {
        private ICategoryRepository _categoryRepository;
        private ICategoryService _categoryService;
        private IApplicationDbContext _memoryDbContext;
        private Guid CountryId = Guid.NewGuid();
        private FakeDbSet<Category> _categories;

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
            var _loggerRepo = Substitute.For<ILogger<CategoryService>>();
            var _userStore = Substitute.For<IUserStore<ApplicationUser>>();
            var _userManager = Substitute.For<UserManager<ApplicationUser>>(_userStore, null, null, null, null, null, null, null, null);


            services.AddTransient<ICategoryRepository, CategoryRepository>();
            var serviceProvider = services.BuildServiceProvider();
            _categoryRepository = new CategoryRepository(_memoryDbContext);
            _categoryService = Substitute.For<CategoryService>
                (_categoryRepository, _loggerRepo, _userManager);

            _categories = new FakeDbSet<Category>(
                    Builder<Category>.CreateListOfSize(10)
                    .All()
                    .With(c => c.ID = Guid.NewGuid())
                    .With(cid => cid.CountryId = CountryId).Build());

            if (_memoryDbContext.Categories.Count() == 0)
            {
                _memoryDbContext.Categories.AddRange(_categories);
                _memoryDbContext.SaveChanges();
            }

        }

        [Test()]
        public void AddTest()
        {
            var count = _memoryDbContext.Categories.Count();
            var categoryCreateModel = Builder<CategoryCreateModel>.CreateNew().Build();

            Assert.IsTrue(_categoryService.Add(categoryCreateModel));
            Assert.IsTrue(_memoryDbContext.Categories.Count() == count + 1);
        }

        [Test()]
        public void DeleteTest()
        {
            int count = _memoryDbContext.Categories.Count();
            var category = _memoryDbContext.Categories.FirstOrDefault();

            Assert.IsTrue(_categoryService.Delete(category.ID));
            Assert.IsTrue(count - 1 == _memoryDbContext.Categories.Count());
        }

        [Test()]
        public void ExistsTest()
        {
            var category = _memoryDbContext.Categories.FirstOrDefault();

            Assert.IsTrue(_categoryService.Exists(category.ID));
        }

        [Test()]
        public void GetCategoriesAsJSTreeTest()
        {
            var entitiesList = _categoryService.GetCategoriesAsJSTree(CountryId);

            Assert.IsTrue(entitiesList.Count > 0);
        }

        [Test()]
        public void GetCategoryAsTreeStructureTest()
        {

            var entitiesList = _categoryService.GetCategoryAsTreeStructure(CountryId);

            Assert.IsTrue(entitiesList.Count > 0);

        }

        [Test()]
        public void GetSubcategoriesIdForCategoryTest()
        {
            Guid parentCategoryId = Guid.NewGuid();
            var childrenCategory = Builder<Category>.CreateListOfSize(2).All()
                .With(c => c.CountryId = CountryId)
                .With(p => p.ParentId = parentCategoryId)
                .Build();

            Category category = Builder<Category>
                .CreateNew()
                    .With(c => c.ID = parentCategoryId)
                    .With(c => c.Children = childrenCategory)
                    .With(cid => cid.CountryId = CountryId).Build();

            _memoryDbContext.Categories.Add(category);
            _memoryDbContext.SaveChanges();

            var lookupsList = _categoryService.GetSubcategoriesIdForCategory(category.ID, CountryId);

            Assert.IsTrue(lookupsList.Count > 0);
        }

        [Test()]
        public void UpdateTest()
        {
            var category = _memoryDbContext.Categories.FirstOrDefault();
            var categoryEditModel = Builder<CategoryEditModel>.CreateNew()
                .With(c => c.ID = category.ID).Build();

            Assert.IsTrue(_categoryService.Update(categoryEditModel));
        }

        [Test()]
        public void GetAllAsLookupTest()
        {
            var entities = _memoryDbContext.Categories.Where(c => c.CountryId == CountryId);
            var lookupsList = _categoryService.GetAllAsLookup(CountryId);

            Assert.IsTrue(lookupsList.First().Id != Guid.Empty);
            Assert.IsTrue(!String.IsNullOrEmpty(lookupsList.First().Name));
        }
    }
}