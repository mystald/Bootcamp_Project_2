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
        public Task<Price> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Price>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Price> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<double> GetByName(string name)
        {
            var result = await _db.Prices.Where(
                price => price.Name == name
            ).SingleOrDefaultAsync();

            if (result == null) throw new Exception("Price not found");

            return result.Value;
        }

        public Task<Price> Insert(Price obj)
        {
            throw new NotImplementedException();
        }

        public Task<Price> Update(int id, Price obj)
        {
            throw new NotImplementedException();
        }

        public async Task<Price> Update(Price obj)
        {
            try
            {
                var oldCost = await _db.Prices.FirstOrDefaultAsync();

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