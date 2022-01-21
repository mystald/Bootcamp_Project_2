using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.Models;

namespace CustomerService.DAL
{
    public interface ICustomer : ICrud<Customer>
    {
        Task<Customer> TopUp(string id, Customer obj);
        Task<Customer> DeductBalanceWhenInsert(int customerId, double fee);
    }
}