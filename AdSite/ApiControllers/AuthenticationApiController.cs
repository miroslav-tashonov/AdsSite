using AdSite.Models;
using AdSite.Models.AccountViewModels;
using AdSite.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AdSite.Models.Extensions;
using AdSite.Models.CRUDModels;
using AdSite.Extensions;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AdSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRoleCountryService _userRoleCountryService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public AuthenticationApiController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationIdentityRole> roleManager,
            IUserRoleCountryService userRoleCountryService,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRoleCountryService = userRoleCountryService;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        [HttpPost, Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var account = await _userManager.FindByNameAsync(model.Email);
                if (account == null)
                {
                    return BadRequest("Account doesnt exist");
                }

                if (_userRoleCountryService.Exists(account.Id, model.CountryId))
                {
                    return Ok(SignIn(account, model.CountryId));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest();
        }

        [HttpPost, Route("register")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, string returnUrl = null)
        {
            var account = _userManager.FindByNameAsync(model.Email).GetAwaiter().GetResult();
            if (account != null)
            {
                return BadRequest("Account already exist");
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRole), UserRole.User));
                if (result.Succeeded)
                {
                    AddToUserRole(user, model.CountryId);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    if (_userRoleCountryService.Exists(user.Id, model.CountryId))
                    {
                        return Ok(SignIn(user, model.CountryId));
                    }
                }
                else
                {
                    result = await _userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        return BadRequest("Failed to delete user after being unable to be added to role.");
                    }
                }
            }

            return BadRequest();
        }

        [HttpPost, Route("update")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] ManageAccountModel model, string returnUrl = null)
        {
            var account = _userManager.FindByNameAsync(model.Email).GetAwaiter().GetResult();
            if (account == null)
            {
                return BadRequest("Account doesnt exist");
            }
            var result = await _userManager.UpdateAsync(account);
            if (result.Succeeded)
            {
                if (_userRoleCountryService.Exists(account.Id, model.CountryId))
                {
                    return Ok(SignIn(account, model.CountryId));
                }
            }
            return BadRequest();
        }

        [HttpPost, Route("resetPassword")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model, string returnUrl = null)
        {
            var account = _userManager.FindByNameAsync(model.Email).GetAwaiter().GetResult();
            if (account == null)
            {
                return BadRequest("Account doesnt exist");
            }

            var result = await _userManager.ChangePasswordAsync(account, model.OldPassword, model.Password);
            if (result.Succeeded)
            {
                if (_userRoleCountryService.Exists(account.Id, model.CountryId))
                {
                    return Ok(SignIn(account, model.CountryId));
                }
            }
            else
            {
                return BadRequest("Password change error");
            }


            return BadRequest("Something went wrong");
        }


        [HttpPost, Route("GetAllUsers")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllUsers(Guid countryId)
        {
            var accounts = _userManager.Users.ToList();
            if (accounts == null)
            {
                return BadRequest("Accounts doesnt exist");
            }
            return Ok(accounts);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User,Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var account = _userManager.FindByIdAsync(id).GetAwaiter().GetResult();
            if(account == null)
            {
                return BadRequest("Account doesnt exist");
            }

            if (_userRoleCountryService.Delete(id))
            {
                if (!_userManager.DeleteAsync(account).GetAwaiter().GetResult().Succeeded)
                {
                    return BadRequest("Cannot delete account");
                }
                return Ok();
            }
            return BadRequest("Delete action failed");
        }


        #region Helper Methods

        private void AddToUserRole(ApplicationUser user, Guid countryId)
        {
            var role = _roleManager.FindByNameAsync(Enum.GetName(typeof(UserRole), UserRole.User))
                .GetAwaiter().GetResult();

            _userRoleCountryService.Add(
                new UserRoleCountryCreateModel
                {
                    ApplicationUserId = user.Id,
                    CountryId = countryId,
                    RoleId = role.Id
                }
            );
        }


        private ApplicationUserWithToken SignIn(ApplicationUser account, Guid countryId)
        {
            string roleId = _userRoleCountryService.GetAll(countryId).Where(x => x.ApplicationUserId == account.Id).First().RoleId;
            var role = _roleManager.FindByIdAsync(roleId).GetAwaiter().GetResult();

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey:Value")));
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, account.UserName),
                    new Claim(ClaimTypes.Role, role.Name)
                };

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("EnvironmentUrl:Value"),
                audience: _configuration.GetValue<string>("EnvironmentUrl:Value"),
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials
            );

            return new ApplicationUserWithToken()
            {
                Email = account.Email,
                Id = account.Id,
                UserName = account.UserName,
                Role = role.Name,
                PhoneNumber = account.PhoneNumber,
                Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            };
        }

        #endregion

    }
}
