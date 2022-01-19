using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Prices.Any())
            {
                context.Prices.Add(
                    new Price
                    {
                        Name = "NormalPricePerKM",
                        Value = 1000,
                    }
                );
            }

            context.SaveChanges();
        }
    }
}