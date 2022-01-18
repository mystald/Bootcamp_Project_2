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

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomer _customer;
        private readonly IMapper _mapper;

        public CustomersController(ICustomer customer, IMapper mapper)
        {
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
    }

}