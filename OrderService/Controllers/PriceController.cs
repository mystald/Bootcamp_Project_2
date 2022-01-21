using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Dto;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PriceController : ControllerBase
    {
        private IPrice _price;

        public PriceController(IPrice price)
        {
            _price = price;
        }

        [HttpPost]
        public async Task<ActionResult<DtoPrice>> SetPrice(DtoPrice input)
        {
            try
            {
                var result = await _price.Update(
                    input.name,
                    new Price
                    {
                        Value = input.value
                    }
                );
                return Ok(
                    new DtoPrice
                    {
                        name = result.Name,
                        value = result.Value,
                    }
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}