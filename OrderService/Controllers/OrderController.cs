using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Dto;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrder _order;
        private IMapper _mapper;

        public OrderController(IOrder order, IMapper mapper)
        {
            _order = order;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder([FromBody] DtoOrderInsert order)
        {
            var result = await _order.Insert(_mapper.Map<Order>(order));
            return Ok(result);
        }

        [HttpPost("{orderId}/accept")]
        public async Task<ActionResult<Order>> AcceptOrder(int orderId, [FromBody] int driverId)
        {
            var result = await _order.Update(
                orderId,
                new Order
                {
                    DriverId = driverId,
                    Status = status.accepted
                }
            );
            return Ok(result);
        }

        [HttpPost("{orderId}/finish")]
        public async Task<ActionResult<Order>> FinishOrder(int orderId)
        {
            var result = await _order.Update(
                orderId,
                new Order
                {
                    Status = status.finished,
                }
            );
            return Ok(result);
        }
    }
}