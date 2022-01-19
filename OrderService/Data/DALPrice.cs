using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data
{
    public class DALPrice : IPrice
    {
        private ApplicationDbContext _db;

        public DALPrice(ApplicationDbContext db)
        {
            _db = db;
        }

        public Price GetByName(string name)
        {
            var result = _db.Prices.Where(
                price => price.Name == name
            ).SingleOrDefault();

            if (result == null) throw new Exception("Price not found");

            return result;
        }

        public async Task<Price> Update(string name, Price obj)
        {
            try
            {
                var oldCost = GetByName(name);

                oldCost.Value = obj.Value;

                await _db.SaveChangesAsync();

                return oldCost;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}