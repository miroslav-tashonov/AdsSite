using AdSite.Data.Repositories;
using AdSite.Models;
using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdSite.Services
{
    public interface ICategoryService
    {
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(CategoryCreateModel category);
        bool Update(CategoryEditModel category);

        //todo: we need to add country as parameter here
        List<Category> GetCategoryTree();
        List<Category> GetCategoryAsTreeStructure();
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
                RecursivelyDeleteCategoryNodes(id);
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

        private void RecursivelyDeleteCategoryNodes(Guid id)
        {
            var entity = _repository.Get(id);
            if (entity != null)
            {
                if (entity.Children != null && entity.Children.Any())
                {
                    foreach (var children in entity.Children.ToList())
                    {
                        RecursivelyDeleteCategoryNodes(children.ID);
                    }
                }

                _repository.Delete(entity.ID);
            }
        }

        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public List<Category> GetCategoryTree()
        {
            return _repository.GetCategoryTree();
        }

        public List<Category> GetCategoryAsTreeStructure()
        {
            return _repository.GetCategoryAsTreeStructure();
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

        private CategoryViewModel MapToViewModel(Category entity)
        {
            CategoryViewModel model = new CategoryViewModel
            {
                Name = entity.Name,

            };

            return model;
        }

        private List<CategoryViewModel> MapToViewModel(List<Category> entities)
        {
            List<CategoryViewModel> model = new List<CategoryViewModel>();
            foreach (var entity in entities)
            {
                model.Add(MapToViewModel(entity));
            }
            return model;
        }

    }
}
