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
                new Customer{FirstName="Spongebob",LastName="Squarepants",BirthDate=DateTime.Parse("1999-01-01"),Balance=5000,UserId=1},
                new Customer{FirstName="Yohan",LastName="Kang",BirthDate=DateTime.Parse("1999-01-01"),Balance=5000,UserId=2},
                new Customer{FirstName="Gaon",LastName="Kim",BirthDate=DateTime.Parse("1999-01-01"),Balance=5000,UserId=3},
                new Customer{FirstName="Yikyung",LastName="Song",BirthDate=DateTime.Parse("1999-01-01"),Balance=5000,UserId=4},
            };

            foreach (var c in customers)
            {
                context.Customers.Add(c);
            }

            context.SaveChanges();

        }
    }
}