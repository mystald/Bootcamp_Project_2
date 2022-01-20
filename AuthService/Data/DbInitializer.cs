using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Data
{
    public class DbInitializer
    {
        public static void Seed(IServiceProvider service)
        {
            var context = service.GetRequiredService<ApplicationDbContext>();

            if (!context.Roles.Any())
            {
                var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

                roleManager.CreateAsync(new IdentityRole("Admin"));

                roleManager.CreateAsync(new IdentityRole("Driver"));

                roleManager.CreateAsync(new IdentityRole("Customer"));
            }
        }
    }
}