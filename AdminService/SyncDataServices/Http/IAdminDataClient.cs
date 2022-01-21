using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminService.Dtos;

namespace AdminService.SyncDataServices.Http
{
    public interface IAdminDataClient
    {
        Task<IEnumerable<DtoCustomerGet>> GetCustomer();
        Task<DtoCustomerGet> GetCustomerById(int id);
        Task<IEnumerable<DriverDto>> GetDriver();
        Task<DriverDto> GetDriverById(int id);
        Task<IEnumerable<DtoOrderOutput>> GetOrder();
        Task<AcceptDriverReturn> ApproveDriver(AcceptDriverDto insert);
        Task<DtoPrice> SetPrice(DtoPrice insert);
        Task<string> BlockCustomer(string userId);
        Task<string> UnblockCustomer(string userId);
        Task<string> BlockDriver(string userId);
        Task<string> UnblockDriver(string userId);
    }
}