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
        private IPrice _price;

        public DALOrder(ApplicationDbContext db, IPrice price)
        {
            _db = db;
            _price = price;
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

        public async Task<IEnumerable<Order>> GetByCustomerId(int customerId)
        {
            var result = await _db.Orders.Where(
                order => order.CustomerId == customerId
            ).ToListAsync();

            if (!result.Any()) throw new Exception("Order not found");

            return result;
        }

        public async Task<Order> GetById(int id)
        {
            var result = await _db.Orders.Where(
                order => order.Id == id
            ).SingleOrDefaultAsync();

            if (result == null) throw new Exception("Order not found");

            return result;
        }

        public double GetFeeByDistance(double distance)
        {
            var feePerKM = _price.GetByName("Normal").Value;

            var fee = Math.Ceiling(distance * feePerKM);

            return fee % 500 >= 250 ? fee + 500 - fee % 500 : fee - fee % 500;
        }

        public async Task<Order> Insert(Order obj)
        {
            try
            {
                obj.Status = status.waiting;

                var distance = obj.StartDest.Distance(obj.EndDest);

                if (distance == 0) throw new Exception("No distance between Starting Position and Destination");

                obj.Distance = distance;
                obj.Fee = GetFeeByDistance(distance);

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
                oldOrder.Status = obj.Status;

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