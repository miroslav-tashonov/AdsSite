using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdSite.Extensions;
using AdSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AdSite.Controllers
{
    public class ManageAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        private readonly int SERVER_ERROR_CODE = 500;

        public ManageAccountController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string searchString, string columnName)
        {
            var allUsers = _userManager.Users?.AsEnumerable();
            return View(allUsers);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var account = _userManager.Users.Where(u => u.Id == id)?.First();
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var account = _userManager.Users.Where(u => u.Id == id)?.First();
            if (account == null)
            {
                return NotFound();
            }

            try
            {
                _userManager.DeleteAsync(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(SERVER_ERROR_CODE).WithError(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }


    }
}