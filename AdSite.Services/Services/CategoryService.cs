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
using AdSite.Mappers;
using AdSite.Models.Models.AdSiteViewModels;

namespace AdSite.Services
{
    public interface ICategoryService
    {
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(CategoryCreateModel category);
        bool Update(CategoryEditModel category);
        List<JSTreeViewModel> GetCategoriesAsJSTree(Guid countryId);
        List<CategoryViewModel> GetCategoryAsTreeStructure(Guid countryId);
        List<LookupViewModel> GetAllAsLookup(Guid countryId);
    }



    public class CategoryService : ICategoryService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryRepository _repository;
        private readonly ILogger _logger;
        public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        public bool Add(CategoryCreateModel entity)
        {
            Guid? parent = entity.ParentId;
            if (parent == Guid.Empty)
                parent = null;

            Category category = new Category
            {
                Type = entity.Type,
                Name = entity.Name,
                ParentId = parent,
                CreatedBy = entity.CreatedBy,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = entity.ModifiedAt,
                ModifiedBy = entity.ModifiedBy,
                CountryId = entity.CountryId
            };


            return _repository.Add(category);
        }

        public bool Delete(Guid id)
        {
            //first recursively delete all child nodes
            try
            {
                _repository.RecursivelyDeleteCategoryNodes(id);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception while deleting categories : {0} - {1} ", ex.StackTrace, ex.Message);
                return false;
            }
            finally
            {
                _repository.SaveChangesResult();
            }

            return true;
        }

        

        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public List<JSTreeViewModel> GetCategoriesAsJSTree(Guid countryId)
        {
            var viewModel = new List<JSTreeViewModel>();

            try
            {
                var entities = _repository.GetCategoryTree(countryId);
                viewModel = JSTreeViewModelMapper.MapToJSTreeViewModel(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return viewModel;
        }

        public List<CategoryViewModel> GetCategoryAsTreeStructure(Guid countryId)
        {
            return CategoryMapper.MapToViewModel(_repository.GetCategoryAsTreeStructure(countryId));
        }
        
        public bool Update(CategoryEditModel entity)
        {
            Guid? parent = entity.ParentId;
            if (parent == Guid.Empty)
                parent = null;

            Category category = _repository.Get(entity.ID);
            category.Name = entity.Name;
            category.Type = entity.Type;
            category.ParentId = parent;

            category.ModifiedAt = entity.ModifiedAt;
            category.ModifiedBy = entity.ModifiedBy;

            return _repository.Update(category);
        }

        public List<LookupViewModel> GetAllAsLookup(Guid countryId)
        {
            return CategoryMapper.MapToLookupViewModel(_repository.GetAll(countryId));
        }


    }
}
