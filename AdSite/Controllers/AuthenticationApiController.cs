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

namespace AdSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationApiController : ControllerBase
    {
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRoleCountryService _userRoleCountryService;
        public AuthenticationApiController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationIdentityRole> roleManager, IUserRoleCountryService userRoleCountryService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRoleCountryService = userRoleCountryService;
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
                if (_userRoleCountryService.Exists(account.Id, Guid.Parse(model.CountryId)))
                {
                    string roleId = _userRoleCountryService.GetAll(Guid.Parse(model.CountryId)).Where(x => x.ApplicationUserId == account.Id).First().RoleId;
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

                    return Ok(user);

                }
                else
                {
                    return BadRequest("This login is invalid in this country.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
