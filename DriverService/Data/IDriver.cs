using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverService.Dtos;
using DriverService.Models;

namespace DriverService.Data
{
    public interface IDriver
    {
        Task<Driver> Insert(Driver obj);
        void UpdatePosition(int id, Driver obj);
        void UpdateAcceptOrder(int id, Driver obj);
        void UpdateFinishOrder(int id, Driver obj);
        void AcceptDriver(int id, Driver obj);
        Task<IEnumerable<Driver>> GetAll();
        Task<Driver> GetById(string id);
        Task<Driver> GetBalanceById(string id);
        Task<Driver> GetProfileById(string id);
        Task<Driver> AddBalanceWhenFinish(int driverId, double addBalance);
        bool SaveChanges();
    }
}
