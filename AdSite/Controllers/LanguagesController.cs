using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdSite.Data;
using AdSite.Models.DatabaseModels;

namespace AdSite.Controllers
{
    public class LanguagesController : Controller
    {

        //private readonly ILanguagesService _languageService;
        //public LanguagesController(ILanguagesService languageService)
        //{
        //    _languageService = languageService;
        //}

        //// GET: Languages
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Languages.ToListAsync());
        //}

        //// GET: Languages/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var language = await _context.Languages
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (language == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(language);
        //}

        //// GET: Languages/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Languages/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CultureId,LanguageName,LanguageShortName,ID")] Language language)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        language.ID = Guid.NewGuid();
        //        _context.Add(language);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(language);
        //}

        //// GET: Languages/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var language = await _context.Languages.FindAsync(id);
        //    if (language == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(language);
        //}

        //// POST: Languages/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("CultureId,LanguageName,LanguageShortName,ID")] Language language)
        //{
        //    if (id != language.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(language);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LanguageExists(language.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(language);
        //}

        //// GET: Languages/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var language = await _context.Languages
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (language == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(language);
        //}

        //// POST: Languages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var language = await _context.Languages.FindAsync(id);
        //    _context.Languages.Remove(language);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LanguageExists(Guid id)
        //{
        //    return _context.Languages.Any(e => e.ID == id);
        //}
    }
}
