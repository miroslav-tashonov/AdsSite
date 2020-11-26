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

        public AuthenticationApiController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationIdentityRole> roleManager,
            IUserRoleCountryService userRoleCountryService,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRoleCountryService = userRoleCountryService;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost, Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid client request");
                }

                var account = _userManager.FindByNameAsync(model.Email).GetAwaiter().GetResult();
                if (account == null)
                {
                    return BadRequest("Account doesnt exist");
                }

                //custom check if account exist in current country/region 
                var user = SignIn(account, Guid.Parse(model.CountryId));

                if (user != null)
                    return Ok(user);
                else
                    return BadRequest("Sign in is invalid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost, Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, string returnUrl = null)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserRole), UserRole.User));
                if (result.Succeeded)
                {
                    AddToUserRole(user, Guid.Parse(model.CountryId));

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);
                    var account = SignIn(user, Guid.Parse(model.CountryId));

                    if (account != null)
                        return Ok(account);
                    else
                        return BadRequest("Sign in is invalid");
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] RegisterViewModel model, string returnUrl = null)
        {
            var account = _userManager.FindByNameAsync(model.Email).GetAwaiter().GetResult();
            if (account == null)
            {
                return BadRequest("Account doesnt exist");
            }

            var result = await _userManager.UpdateAsync(account);
            if (result.Succeeded)
            {
                var user = SignIn(account, Guid.Parse(model.CountryId));
                if (user != null)
                    return Ok(user);
                else
                    return BadRequest("Sign in is invalid");
            }

            return BadRequest();
        }

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
            if (_userRoleCountryService.Exists(account.Id, countryId))
            {
                string roleId = _userRoleCountryService.GetAll(countryId).Where(x => x.ApplicationUserId == account.Id).First().RoleId;
                var role = _roleManager.FindByIdAsync(roleId).GetAwaiter().GetResult();

                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, account.UserName),
                    new Claim(ClaimTypes.Role, role.Name)
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44321",
                    audience: "https://localhost:44321",
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: signingCredentials
                );


                ApplicationUserWithToken user = new ApplicationUserWithToken()
                {
                    Email = account.Email,
                    Id = account.Id,
                    UserName = account.UserName,
                    Role = role.Name,
                    PhoneNumber = account.PhoneNumber,
                    Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                    PasswordHash = account.PasswordHash,
                };


                return user;
            }
            else
            {
                return null;
            }
        }

    }
}
