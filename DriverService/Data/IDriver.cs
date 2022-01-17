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
        void UpdatePosition(int id, Driver obj);
        void UpdateAcceptOrder(int id, Driver obj);
        void UpdateFinishOrder(int id, Driver obj);

        IEnumerable<Driver> GetAllDrivers();
        IEnumerable<GetDriverBalanceDto> GetBalanceById(string id);
        IEnumerable<GetDriverProfileDto> GetProfileById(string id);
        bool SaveChanges();
        
    }
}
