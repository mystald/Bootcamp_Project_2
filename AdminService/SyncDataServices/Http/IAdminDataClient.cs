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
        Task<IEnumerable<DriverDto>> GetDriver();
        Task<IEnumerable<DtoOrderOutput>> GetOrder();
    }
}