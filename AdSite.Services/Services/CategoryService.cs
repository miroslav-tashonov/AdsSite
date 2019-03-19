using AdSite.Data.Repositories;
using AdSite.Models.DatabaseModels;
using AdSite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdSite.Services
{
    public interface ICategoryService
    {
        CategoryViewModel Get(Guid Id);
        List<CategoryViewModel> GetAll();
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Add(CategoryCreateModel category);
        bool Update(CategoryEditModel category);
        List<Category> GetBlogCategoryTree();
    }



    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public bool Add(CategoryCreateModel entity)
        {
            Guid? parent = entity.ParentId;
            if (parent == Guid.Empty)
                parent = null;
            Category category = new Category
            {
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Type = entity.Type,
                Name = entity.Name,
                ParentId = parent,
                CountryId = new Guid("9B0CBFD6-0070-4285-B353-F13189BD2291")
            };


            return _repository.Add(category);
        }

        public bool Delete(Guid id)
        {
            //first recursively delete all child nodes
            RecursivelyDeleteCategoryNodes(id);

            return true;
        }

        public bool Exists(Guid id)
        {
            return _repository.Exists(id);
        }

        public CategoryViewModel Get(Guid id = new Guid())
        {
            var entity = _repository.Get(id);

            return MapToViewModel(entity);

        }

        public List<CategoryViewModel> GetAll()
        {
            var entities = _repository.GetAll();

            return MapToViewModel(entities);
        }


        public bool Update(CategoryEditModel entity)
        {
            Guid? parent = entity.ParentId;
            if (parent == Guid.Empty)
                parent = null;

            Category category = _repository.Get(entity.ID);
            category.ModifiedAt = DateTime.Now;
            category.Name = entity.Name;
            category.Type = entity.Type;
            category.ParentId = parent;

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


        public List<Category> GetBlogCategoryTree()
        {
            return _repository.GetBlogCategoryTree();
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
