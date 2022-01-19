using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private IPrice _price;

        public PriceController(IPrice price)
        {
            _price = price;
        }

        [HttpPost]
        public async Task<ActionResult<double>> SetPrice(double newPrice)
        {
            try
            {
                var result = await _price.Update(
                    new Price
                    {
                        Value = newPrice
                    }
                );
                return Ok(result.Value);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}