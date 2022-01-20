using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.Models;

namespace CustomerService.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
                new Customer{FirstName="Spongebob",LastName="Squarepants",BirthDate=DateTime.Parse("1999-01-01"),Balance=5000},
            };

            foreach (var c in customers)
            {
                context.Customers.Add(c);
            }

            context.SaveChanges();

        }
    }
}