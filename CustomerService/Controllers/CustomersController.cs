using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CustomerService.DAL;
using CustomerService.Models;
using CustomerService.Dtos;
using AutoMapper;
using Microsoft.Extensions.Options;
using CustomerService.Helpers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomer _customer;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomersController(ICustomer customer, IMapper mapper, IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _appSettings = appSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCustomerDto>>> GetAllCustomer()
        {
            var customers = await _customer.GetAll();
            var dtos = _mapper.Map<IEnumerable<GetCustomerDto>>(customers);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDto>> GetCustomerById(int id)
        {
            var result = await _customer.GetById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<GetCustomerDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult<GetCustomerDto>> Post([FromBody] GetCustomerForCreateDto getCustomerForCreateDto)
        {
            try
            {
                var student = _mapper.Map<Customer>(getCustomerForCreateDto);
                var result = await _customer.Insert(student);
                var studentReturn = _mapper.Map<GetCustomerDto>(result);
                return Ok(studentReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _customer.Delete(id.ToString());
                return Ok($"Data student {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Balance")]
        public async Task<ActionResult<IEnumerable<GetBalanceDto>>> ViewBalance()
        {
            var customers = await _customer.GetAll();
            var dtos = _mapper.Map<IEnumerable<GetBalanceDto>>(customers);
            return Ok(dtos);
        }

        [HttpGet("{id}/Balance")]
        public async Task<ActionResult<GetBalanceDto>> ViewBalanceById(int id)
        {
            var result = await _customer.GetById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<GetBalanceDto>(result));
        }

        [HttpPut("{id}/TopUp")]
        public async Task<ActionResult<GetBalanceDto>> TopUpBalance(int id, [FromBody] GetBalanceForCreateDto getBalanceForCreateDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(getBalanceForCreateDto);
                var result = await _customer.Update(id.ToString(), customer);
                var customerdto = _mapper.Map<GetBalanceDto>(result);
                return Ok(customerdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/OrderHistory")]
            public async Task<ActionResult<DtoOrderOutput>> ViewOrderHistory(int id)
            {
                var result = await _customer.GetById(id.ToString());
                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<DtoOrderOutput>(result));
            }

        [HttpGet("{id}/Fee")]
            public async Task<ActionResult<DtoFee>> CheckFee(int id)
            {
                var result = await _customer.GetById(id.ToString());
                if (result == null)
                    return NotFound();

                return Ok(_mapper.Map<DtoOrderOutput>(result));
            }


        [HttpPost("Order")]
        public async Task<ActionResult<GetBalanceDto>> CreateOrder(DtoOrderInsert dtoOrderInsert)
        {
            try
            {
            var result = await _customer.GetById(dtoOrderInsert.CustomerId.ToString());
            if (result != null)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
          clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

          using (var client = new HttpClient(clientHandler))
          {
            var json = JsonSerializer.Serialize(dtoOrderInsert);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_appSettings.OrderService+ "/api/v1", data);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Order Service was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to Order Service failed");
            }
          }

        }
        return Ok(dtoOrderInsert);
      }
      catch (System.Exception ex)
      {
        Console.WriteLine(ex);
        return BadRequest(ex.Message);
      }

    }

    }
}