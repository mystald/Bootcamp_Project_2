using System;
using System.Threading.Tasks;
using CustomerService.Dtos;
using System.Collections.Generic;

namespace CustomerService.SyncDataServices.Http
{
    public interface IOrderDataClient
    {
        Task<DtoOrderOutput> CreateOrder(DtoOrderInsert ins);
        Task<IEnumerable<DtoOrderOutput>> GetOrderHistory(int CustomerId);
        Task<DtoFeeOutput> CheckFee(DtoFeeInsert insert);
    }
}
