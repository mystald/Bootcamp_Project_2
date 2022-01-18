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
        public async Task<ActionResult<IEnumerable<GetBalanceDto>>> ViewBalance()
        {
            var customers = await _customer.GetAll();
            var dtos = _mapper.Map<IEnumerable<GetBalanceDto>>(customers);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBalanceDto>> ViewBalanceById(int id)
        {
            var result = await _customer.GetById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<GetBalanceDto>(result));
        }

        [HttpPut("{id}")]
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

        // [HttpGet]
        //     public async Task<ActionResult<DtoOrderOutput>> ViewOrderHistory()
        //     {
        //         var results = await _customer.GetAll();
        //         return Ok(_mapper.Map<IEnumerable<DtoOrderOutput>>(results));
        //     }

        [HttpPost]
        public async Task<ActionResult<GetBalanceDto>> CreateOrder(DtoOrderInsert dtoOrderInsert)
        {
            try
            {
            var customerModel = _mapper.Map<Customer>(dtoOrderInsert);
            var result = await _customer.Insert(customerModel);
            if (result != null)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
          clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

          using (var client = new HttpClient(clientHandler))
          {
            string token = Request.Headers["Authorization"];
            string[] tokenWords = token.Split(' ');
            var order = new DtoOrderInsert
            {
              CustomerId = result.Id,
              startDest = result.StartDest,
              endDest = result.EndDest
              
            };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenWords[1]);
            var json = JsonSerializer.Serialize(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_appSettings.OrderService, data);
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
        return Ok(_mapper.Map<DtoOrderInsert>(result));
      }
      catch (System.Exception ex)
      {
        Console.WriteLine(ex);
        return BadRequest(ex.Message);
      }

    }

    }
}