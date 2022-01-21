using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverService.Dtos;
using DriverService.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Data
{
    public class DriverDAL : IDriver
    {
        private readonly ApplicationDbContext _db;
        public DriverDAL(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get All Driver
        public async Task<IEnumerable<Driver>> GetAll()
        {
            var results = await _db.Drivers.OrderBy(d => d.FirstName).ToListAsync();
            return results;
        }

        //Get Balance Driver By Id
        public async Task<Driver> GetBalanceById(string id)
        {
            var results = await (from d in _db.Drivers
                                 where d.Id == Convert.ToInt32(id)
                                 select d).SingleOrDefaultAsync();
            if (results == null) throw new Exception($"Data {id} tidak temukan !");

            return results;
        }

        //Get Profile Driver By Id
        public async Task<Driver> GetProfileById(string id)
        {
            var results = await (from d in _db.Drivers
                                 where d.Id == Convert.ToInt32(id)
                                 select d).SingleOrDefaultAsync();
            if (results == null) throw new Exception($"Data {id} tidak temukan !");

            return results;
        }

        //Update Position Driver By Id
        public void UpdatePosition(int id, Driver obj)
        {
            var result = _db.Drivers.FirstOrDefault(p => p.Id == id);
            result.Latitude = obj.Latitude;
            result.Longitude = obj.Longitude;
            _db.SaveChanges();
        }

        //Update Accept Order -> Communication with Order Service
        public void UpdateAcceptOrder(int id, Driver obj)
        {

            throw new NotImplementedException();
        }

        //Update Finish Order -> Communication with Order Service
        public void UpdateFinishOrder(int id, Driver obj)
        {
            throw new NotImplementedException();
        }

        //Save Changes
        public bool SaveChanges()
        {
            return (_db.SaveChanges() >= 0);
        }

        //Accept Driver
        public void AcceptDriver(int id, Driver obj)
        {
            var result = _db.Drivers.FirstOrDefault(p => p.Id == id);
            result.IsApprove = obj.IsApprove;
            _db.SaveChanges();
        }

        //Insert Driver
        public async Task<Driver> Insert(Driver obj)
        {
            try
            {
                _db.Drivers.Add(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        //Get Driver By Id
        public async Task<Driver> GetById(string id)
        {
            var result = await _db.Drivers.Where(s => s.Id == Convert.ToInt32(id)).SingleOrDefaultAsync<Driver>();
            if (result != null)
                return result;
            else
                throw new Exception("Data tidak ditemukan !");
        }

        //Add Balance When Finish
        public async Task<Driver> AddBalanceWhenFinish(int driverId, double addBalance)
        {
            var result = _db.Drivers.FirstOrDefault(p => p.Id == driverId);
            result.Balance += addBalance;
            await _db.SaveChangesAsync();
            Console.WriteLine(result.Balance);
            Console.WriteLine(addBalance);
            Console.WriteLine(result.Balance += addBalance);
            return result;
        }
    }
}