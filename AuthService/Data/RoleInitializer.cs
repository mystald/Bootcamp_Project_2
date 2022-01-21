using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using static Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions;

namespace AuthService.Data
{
    public class RoleInitializer
    {
        public async static Task Seed(IServiceProvider service)
        {
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));

                await roleManager.CreateAsync(new IdentityRole("Driver"));

                await roleManager.CreateAsync(new IdentityRole("Customer"));

                Console.WriteLine("Role Seeded");
            }
        }
    }
}