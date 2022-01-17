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
        public IEnumerable<Driver> GetAllDrivers()
        {
            return _db.Drivers.ToList();
        }

        //Get Balance Driver By Id
        public async Task<IEnumerable<GetDriverBalanceDto>> GetBalanceById(string id)
        {
            var results = await (from d in _db.Drivers
                                 where d.Id == Convert.ToInt32(id)
                                 orderby d.FirstName ascending
                                 select new GetDriverBalanceDto()
                                 {
                                     FirstName = d.FirstName,
                                     LastName = d.LastName,
                                     Balance = d.Balance
                                 }).ToListAsync();
            if (results == null) throw new Exception($"Data {id} tidak temukan !");

            return results;
        }

        //Get Profile Driver By Id
        public async Task<IEnumerable<GetDriverProfileDto>> GetProfileById(string id)
        {
            var results = await (from d in _db.Drivers
                                 where d.Id == Convert.ToInt32(id)
                                 orderby d.FirstName ascending
                                 select new GetDriverProfileDto()
                                 {
                                     FirstName = d.FirstName,
                                     LastName = d.LastName,
                                     BirthDate = d.BirthDate,
                                     Position = d.Position
                                 }).ToListAsync();
            if (results == null) throw new Exception($"Data {id} tidak temukan !");

            return results;
        }

        //Update Position
        public void UpdatePosition(int id, Driver obj)
        {
            var result = _db.Drivers.FirstOrDefault(d => d.Id == id);
            result.Position = obj.Position;
            _db.SaveChanges();
        }

        //Update Accept Order -> Communication with Order Service
        public void UpdateAcceptOrder(int id, Driver obj)
        {
            throw new NotImplementedException();
        }

        //Update Finisih Order -> Communication with Order Service
        public void UpdateFinishOrder(int id, Driver obj)
        {
            throw new NotImplementedException();
        }
        
        //Save Changes
        public bool SaveChanges()
        {
            return (_db.SaveChanges() >= 0);
        }
        
    }
}