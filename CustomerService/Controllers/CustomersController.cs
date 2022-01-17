using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.DAL;
using CustomerService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private ICustomer<Customer> _customer;
        public CustomersController(ICustomer<Customer> customer)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }
    }

}