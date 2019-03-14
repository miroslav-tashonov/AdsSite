using AdSite.Models.DatabaseModels;
using AdSite.Models.ViewModels;
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
        List<Category> GetBlogCategoryTree();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
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
            var category = _context.Categories.FirstOrDefaultAsync(m => m.ID == id);
            var result = category.Result;
            if (result == null)
            {
                throw new Exception();
            }

            _context.Categories.Remove(result);
            return SaveChangesResult();
        }

        public bool Exists(Guid id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }

        public Category Get(Guid id)
        {
            var category = _context.Categories.FirstOrDefaultAsync(m => m.ID == id);
            var result = category.Result;
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public List<Category> GetAll()
        {
            var categories = _context.Categories.ToListAsync();

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
                throw new Exception();
            }
            return true;
        }

        public List<Category> GetBlogCategoryTree()
        {
            return _context.Categories
                           .Include(i => i.Children)
                           .AsEnumerable()
                           .Where(x => x.Parent == null)
                           .ToList();
        }
    }
}
