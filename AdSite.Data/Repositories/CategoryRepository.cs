﻿using AdSite.Models.DatabaseModels;
using AdSite.Models.CRUDModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSite.Data.Repositories
{

    public interface ICategoryRepository : IRepository<Category>
    {
        Guid? GetCategoryParentId(Guid categoryId);
        List<Category> GetCategoryTree(Guid countryId);
        List<Category> GetCategoryAsTreeStructure(Guid countryId);
        List<Guid> GetSubcategoriesIdForCategory(Guid categoryId, Guid countryId);
        bool SaveChangesResult();
        void RecursivelyDeleteCategoryNodes(Guid id);
    }

    public class CategoryRepository : ICategoryRepository
    {

        private readonly IApplicationDbContext _context;

        public CategoryRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Category entity)
        {
            _context.Add(entity);
            return SaveChangesResult();
        }

        public bool Delete(Guid id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefaultAsync(m => m.ID == id);
                var result = category.Result;
                if (result == null)
                {
                    throw new Exception();
                }

                _context.Categories.Remove(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return SaveChangesResult();
            return true;
        }

        public bool Exists(Guid id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }

        public Category Get(Guid id)
        {
            var category = _context.Categories.Include(c => c.Children).FirstOrDefaultAsync(m => m.ID == id);
            var result = category.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Category> GetAll(Guid countryId)
        {
            var categories = _context.Categories.Where(category => category.CountryId == countryId).ToListAsync();

            return categories.Result;
        }


        public bool Update(Category entity)
        {
            _context.Update(entity);
            return SaveChangesResult();
        }

        public bool SaveChangesResult()
        {
            try
            {
                var result = _context.SaveChangesAsync();
                if (result.Result == 0)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public void RecursivelyDeleteCategoryNodes(Guid id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                if (entity.Children != null && entity.Children.Any())
                {
                    foreach (var children in entity.Children.ToList())
                    {
                        RecursivelyDeleteCategoryNodes(children.ID);
                    }
                }

                Delete(entity.ID);
            }
        }

        public List<Category> GetCategoryTree(Guid countryId)
        {
            return _context.Categories.Where(category => category.CountryId == countryId).ToList();
        }

        public List<Category> GetCategoryAsTreeStructure(Guid countryId)
        {
            return _context.Categories.Include(i => i.Children).AsEnumerable().Where(p => p.Parent == null && p.CountryId == countryId).ToList();
        }

        public List<Guid> GetSubcategoriesIdForCategory(Guid categoryId, Guid countryId)
        {
            var items = _context.Categories.Include(i => i.Children).AsEnumerable().Where(c => c.ID == categoryId && c.CountryId == countryId).ToList();

            return RecursivelyGetChildrenGuids(items);
        }

        private List<Guid> RecursivelyGetChildrenGuids(List<Category> items)
        {
            var categoriesList = new List<Guid>();

            foreach (var item in items)
            {
                categoriesList.Add(item.ID);
                if (item.Children != null && item.Children.Count > 0)
                {
                    categoriesList.AddRange(RecursivelyGetChildrenGuids(item.Children.ToList()));
                }
            }

            return categoriesList;
        }

        public Guid? GetCategoryParentId(Guid categoryId)
        {
            var category = _context.Categories.FirstOrDefaultAsync(m => m.ID == categoryId);
            var result = category.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result.ParentId;
        }
    }
}
