using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Data
{
    public interface IOrder : ICrud<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerId(int customerId);
    }
}