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
using CustomerService.SyncDataServices.Http;
using CustomerService.Kafka;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ICustomer _customer;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOrderDataClient _dataClient;

        public CustomersController(ICustomer customer, IMapper mapper,
        IOptions<AppSettings> appSettings, IHttpClientFactory httpClientFactory,
        IOrderDataClient dataClient, IConfiguration config)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _appSettings = appSettings.Value;
            _httpClientFactory = httpClientFactory;
            _dataClient = dataClient;
            configuration = config;
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
                var result = await _customer.TopUp(id.ToString(), customer);
                var customerdto = _mapper.Map<GetBalanceDto>(result);
                return Ok(customerdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{CustomerId}/OrderHistory")]
        public async Task<ActionResult<IEnumerable<DtoOrderOutput>>> ViewOrderHistory(int CustomerId)
        {
            try
            {
                var result = await _customer.GetById(CustomerId.ToString());
                if (result != null)
                {
                    var orderhistory = await _dataClient.GetOrderHistory(CustomerId);
                    return Ok(orderhistory);
                }
                return NotFound();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Fee")]
        public async Task<ActionResult<DtoFeeOutput>> CheckFee(DtoFeeInsert input)
        {
            try
            {
                var fee = await _dataClient.CheckFee(input);
                return Ok(fee);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Order")]
        public async Task<ActionResult<DtoOrderInsert>> CreateOrder(DtoOrderInsert dtoOrderInsert)
        {
            try
            {
                var result = await _customer.GetById(dtoOrderInsert.CustomerId.ToString());
                if (result != null)
                {
                    var fee = await _dataClient.CheckFee(
                        new DtoFeeInsert{
                            StartDest = dtoOrderInsert.startDest,
                            EndDest = dtoOrderInsert.endDest                      
                        }
                    );
                    if (result.Balance < fee.fee) return BadRequest("Saldo tidak cukup");
                    var orderResult = await _dataClient.CreateOrder(dtoOrderInsert);
                    await _customer.DeductBalanceWhenInsert(result.Id, orderResult.fee);

                    var key = $"NewOrder-{DateTime.Now.ToString()}";
                    var val = JObject.FromObject(_mapper.Map<DtoOrderForKafka>(orderResult)).ToString(Formatting.None);
                    await KafkaHelper.SendMessage(configuration, "CreateOrderCustomer", key, val);
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