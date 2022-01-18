using System;
using System.Threading.Tasks;
using CustomerService.Dtos;

namespace CustomerService.SyncDataServices.Http
{
    public interface IOrderDataClient
    {
        Task CreateOrder(DtoOrderInsert ins);
        Task<DtoOrderOutput> GetOrderHistory(int CustomerId);
        Task<DtoFee> CheckFee(int CustomerId);
    }
}
