using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Data
{
    public class DALOrder : IOrder
    {
        private ApplicationDbContext _db;

        public DALOrder(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Order> Delete(int id)
        {
            try
            {
                var oldOrder = await GetById(id);

                _db.Remove(oldOrder);

                await _db.SaveChangesAsync();

                return oldOrder;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            var result = await _db.Orders.Where(
                order => order.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new Exception("Order not found");

            return result;
        }

        public async Task<Order> Insert(Order obj)
        {
            try
            {
                var result = await _db.Orders.AddAsync(obj);

                await _db.SaveChangesAsync();

                return result.Entity;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Order> Update(int id, Order obj)
        {
            try
            {
                var oldOrder = await GetById(id);

                if (obj.DriverId != null) oldOrder.DriverId = obj.DriverId;
                if (obj.Status != null) oldOrder.Status = obj.Status;

                await _db.SaveChangesAsync();

                return oldOrder;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}