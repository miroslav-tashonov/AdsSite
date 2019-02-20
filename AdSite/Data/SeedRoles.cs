using AdSite.Extensions;
using AdSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdSite.Data
{
    public class SeedRoles
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SeedRoles>>();
            logger.LogInformation("adding customs roles");


            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = {
                Enum.GetName(typeof(UserRole) ,UserRole.Admin),
                Enum.GetName(typeof(UserRole), UserRole.User),
                Enum.GetName(typeof(UserRole), UserRole.Viewer)
            };

            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                logger.LogInformation("creating the roles and seeding them to the database");
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            
        }
    }
}
