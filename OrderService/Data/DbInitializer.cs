using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            var normalPrice = context.Prices.Where(price => price.Name == "Normal").SingleOrDefault();

            if (normalPrice == null)
            {
                context.Prices.Add(
                    new Price
                    {
                        Name = "Normal",
                        Value = 1000,
                    }
                );
            }

            context.SaveChanges();
        }
    }
}