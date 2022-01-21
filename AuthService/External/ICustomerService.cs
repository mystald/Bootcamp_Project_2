using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Dto;

namespace AuthService.External
{
    public interface ICustomerService
    {
        Task InsertCustomer(DtoCustomer customer);
    }
}