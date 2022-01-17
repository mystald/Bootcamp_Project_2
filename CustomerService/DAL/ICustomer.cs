using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.Models;

namespace CustomerService.DAL
{
    public interface ICustomer<Customer>
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(string id);
        Task<Customer> Insert(Customer obj);
        Task<Customer> Update(string id, Customer obj);
        Task Delete(string id);
    }
}