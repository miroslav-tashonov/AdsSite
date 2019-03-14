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
        bool Add(Category category);
        bool Update(Category category);
        List<Category> GetBlogCategoryTree();
    }



    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public bool Add(Category category)
        {
            return _repository.Update(category);            
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
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


        public bool Update(Category category)
        {
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


        private List<CategoryViewModel> MapToViewModel(List<Category> entities)
        {
            List<CategoryViewModel> model = new List<CategoryViewModel>();
            foreach(var entity in entities)
            {
                model.Add(MapToViewModel(entity));
            }
            return model;
        }

    }
}
