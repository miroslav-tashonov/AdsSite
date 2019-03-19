using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdSite.Data;
using AdSite.Models.DatabaseModels;
using AdSite.Services;
using AdSite.Models.Models.AdSiteViewModels;
using AdSite.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace AdSite.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return View(_categoryService.GetAll());
        }

        // GET: Categories/Details
        public IActionResult Details()
        {
            var categories = _categoryService.GetBlogCategoryTree();
            var mappedJSTreeCategories = JSTreeViewModel.MapToJSTreeViewModel(categories);
            var viewModel = new CategoryFilterComponentViewModel { ComponentCategories = mappedJSTreeCategories };

            return View(viewModel);
        }
        
        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody]CategoryCreateModel entity)
        {
            if (ModelState.IsValid)
            {
                bool statusResult = _categoryService.Add(entity);
                if(statusResult)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return StatusCode(500);
            }
        }
        
        // POST: Categories/Edit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromBody]CategoryEditModel entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   _categoryService.Update(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(entity.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }
        

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _categoryService.Delete(id);
            return RedirectToAction(nameof(Details));
        }

        private bool CategoryExists(Guid id)
        {
            return _categoryService.Exists(id);
        }
    }
}
