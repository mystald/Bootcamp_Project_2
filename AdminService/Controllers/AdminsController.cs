using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using AdminService.Dtos;
using AdminService.SyncDataServices.Http;

namespace AdminService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IAdminDataClient _dataClient;
        public AdminsController(IAdminDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        [HttpGet("Customer")]
        public async Task<ActionResult<IEnumerable<DtoCustomerGet>>> GetCustomer()
        {
            try
            {
                var customer = await _dataClient.GetCustomer();
                return Ok(customer);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Driver")]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetDriver()
        {
            try
            {
                var customer = await _dataClient.GetDriver();
                return Ok(customer);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}