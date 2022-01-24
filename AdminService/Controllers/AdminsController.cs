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
using Microsoft.AspNetCore.Authorization;

namespace AdminService.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet("Customer/{id}")]
        public async Task<ActionResult<DtoCustomerGet>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _dataClient.GetCustomerById(id);
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
                var driver = await _dataClient.GetDriver();
                return Ok(driver);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Driver/{id}")]
        public async Task<ActionResult<DriverDto>> GetDriverById(int id)
        {
            try
            {
                var driver = await _dataClient.GetDriverById(id);
                return Ok(driver);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Order")]
        public async Task<ActionResult<IEnumerable<DtoOrderOutput>>> GetOrder()
        {
            try
            {
                var order = await _dataClient.GetOrder();
                return Ok(order);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ApproveDriver")]
        public async Task<ActionResult<AcceptDriverReturn>> ApproveDriver(AcceptDriverDto insert)
        {
            try
            {
                var acc = await _dataClient.ApproveDriver(insert);
                return Ok(acc);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Price")]
        public async Task<ActionResult<DtoPrice>> SetPrice(DtoPrice insert)
        {
            try
            {
                var price = await _dataClient.SetPrice(insert);
                return Ok(price);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Customer/{id}/Block")]
        public async Task<ActionResult<DtoCustomerGet>> BlockCustomer(int id)
        {
            try
            {
                var customer = await _dataClient.GetCustomerById(id);
                var block = await _dataClient.BlockCustomer(customer.userId);
                return Ok(block);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Customer/{id}/Unblock")]
        public async Task<ActionResult<DtoCustomerGet>> UnblockCustomer(int id)
        {
            try
            {
                var customer = await _dataClient.GetCustomerById(id);
                var unblock = await _dataClient.UnblockCustomer(customer.userId);
                return Ok(unblock);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Driver/{id}/Block")]
        public async Task<ActionResult<DriverDto>> BlockDriver(int id)
        {
            try
            {
                var driver = await _dataClient.GetDriverById(id);
                var block = await _dataClient.BlockCustomer(driver.userId);
                return Ok(block);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Driver/{id}/Unblock")]
        public async Task<ActionResult<DriverDto>> UnblockDriver(int id)
        {
            try
            {
                var driver = await _dataClient.GetDriverById(id);
                var unblock = await _dataClient.UnblockDriver(driver.userId);
                return Ok(unblock);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }


    }
}