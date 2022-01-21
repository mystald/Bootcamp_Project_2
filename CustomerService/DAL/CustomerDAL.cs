using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerService.Data;
using CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.DAL
{
    public class CustomerDAL : ICustomer
    {
        private ApplicationDbContext _db;
        IHttpClientFactory _httpClientFactory;

        public CustomerDAL(ApplicationDbContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
            _httpClientFactory = httpClientFactory;
            
        }
        public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result == null) throw new Exception("Data tidak ditemukan!");
            try
            {
                _db.Customers.Remove(result);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            var results = await _db.Customers.OrderBy(d => d.FirstName).ToListAsync();
            return results;
        }

        public async Task<Customer> GetById(string id)
        {
            var result = await _db.Customers.Where(s => s.Id == Convert.ToInt32(id)).SingleOrDefaultAsync<Customer>();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak ditemukan !");
        }

        public async Task<Customer> Insert(Customer obj)
        {
            try
            {
                _db.Customers.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task<Customer> Update(string id, Customer obj)
        {
            try
            {
                var result = await GetById(id);
                result.FirstName = obj.FirstName;
                result.LastName = obj.LastName;
                result.BirthDate = obj.BirthDate;
                result.Balance = obj.Balance;
                await _db.SaveChangesAsync();
                obj.Id = Convert.ToInt32(id);
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task<Customer> TopUp(string id, Customer obj)
         {
            var result = await GetById(id);
            result.Balance += obj.Balance;
            await _db.SaveChangesAsync();
            return result;
        }

        public async Task<Customer> DeductBalanceWhenInsert(int customerId, double fee)
        {
            var result = _db.Customers.FirstOrDefault(p => p.Id == customerId);
            result.Balance -= fee;
            await _db.SaveChangesAsync();
            return result;
        }
    }
}