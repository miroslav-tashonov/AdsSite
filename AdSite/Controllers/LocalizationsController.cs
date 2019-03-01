using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdSite.Data;
using AdSite.Models.DatabaseModels;
using AdSite.Services.LocalizationService;

namespace AdSite.Controllers
{
    public class LocalizationsController : Controller
    {
        private readonly ILocalizationService _localizationService;

        public LocalizationsController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        // GET: Localizations
        public IActionResult Index()
        {
            return View(_localizationService.GetAll());
        }

        // GET: Localizations/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localization = _localizationService.Get((Guid)id);

            return View(localization);
        }

        // GET: Localizations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Localizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Localization localization)
        {
            if (ModelState.IsValid)
            {
                _localizationService.Add(localization);
            }
            return View(localization);
        }

        // GET: Localizations/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localization = _localizationService.Get((Guid)id);
            if (localization == null)
            {
                return NotFound();
            }
            return View(localization);
        }

        // POST: Localizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, Localization localization)
        {
            if (id != localization.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _localizationService.Update(localization);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalizationExists(localization.ID))
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
            return View(localization);
        }

        // GET: Localizations/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localization = _localizationService.Get((Guid)id);
            if (localization == null)
            {
                return NotFound();
            }

            return View(localization);
        }

        // POST: Localizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _localizationService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool LocalizationExists(Guid id)
        {
            return _localizationService.Exists(id);
        }
    }
}
