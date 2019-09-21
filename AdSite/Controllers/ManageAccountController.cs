﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdSite.Extensions;
using AdSite.Models;
using AdSite.Models.AccountViewModels;
using AdSite.Models.Extensions;
using AdSite.Services;
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
        private readonly IEmailSender _emailSender;
        private readonly ILocalizationService _localizationService;

        private int CultureId = Thread.CurrentThread.CurrentCulture.LCID;

        private readonly int SERVER_ERROR_CODE = 500;
        private string LOCALIZATION_SUCCESS_DEFAULT => _localizationService.GetByKey("SuccessMessage_Default", CultureId);
        private string LOCALIZATION_ERROR_DEFAULT => _localizationService.GetByKey("ErrorMessage_Default", CultureId);
        private string LOCALIZATION_WARNING_SELF_CHANGEROLE => _localizationService.GetByKey("WarningMessage_Self_RoleChange", CultureId);
        private string LOCALIZATION_ERROR_NOT_FOUND => _localizationService.GetByKey("ErrorMessage_NotFound", CultureId);

        public ManageAccountController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ILocalizationService localizationService,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _logger = logger;
            _localizationService = localizationService;
            _emailSender = emailSender;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index(string searchString, string columnName)
        {
            var allUsers = _userManager.Users?.AsEnumerable();
            return View(allUsers);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult ChangeRole(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            var account = _userManager.Users.Where(u => u.Id == id)?.First();
            if (account == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            var roles = _userManager.GetRolesAsync(account)?.Result;
            ChangeRoleModel model = new ChangeRoleModel
            {
                Id = account.Id,
                Email = account.Email,
                Username = account.UserName,
                Roles = roles == null ? new List<string>() : roles.ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeRole(string id, string role)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            string parsedRole = Enum.Parse(typeof(UserRole), role.ToString()).ToString();
            if (String.IsNullOrEmpty(parsedRole))
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            var user = _userManager.GetUserAsync(User).Result;
            if (user.Id == id)
            {
                return StatusCode(SERVER_ERROR_CODE).WithWarning(LOCALIZATION_WARNING_SELF_CHANGEROLE);
            }

            var account = _userManager.Users.Where(u => u.Id == id)?.First();
            if (account == null)
            {
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
            }

            var roles = _userManager.GetRolesAsync(account).Result;
            if (roles.Count > 0)
                _userManager.RemoveFromRolesAsync(account, roles.ToArray());

            var addingresult = _userManager.AddToRoleAsync(account, parsedRole);
            if (!addingresult.Result.Succeeded)
            {
                return StatusCode(SERVER_ERROR_CODE).WithError(LOCALIZATION_ERROR_DEFAULT);
            }


            return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
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
                return NotFound().WithError(LOCALIZATION_ERROR_NOT_FOUND);
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

            return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
        }


        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRole), UserRole.User));
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                        await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                        _logger.LogInformation("User created a new account with password.");
                        return RedirectToAction(nameof(Index)).WithSuccess(LOCALIZATION_SUCCESS_DEFAULT);
                    }
                    else
                    {
                        _logger.LogInformation("Failed to add user to role. Deleting user");
                        result = await _userManager.DeleteAsync(user);
                        if (!result.Succeeded)
                        {
                            _logger.LogInformation("Failed to delete user after being unable to be added to role.");
                            throw new Exception("Failed to delete user after being unable to be added to role.");
                        }

                    }
                }
                AddErrors(result);
            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


    }
}