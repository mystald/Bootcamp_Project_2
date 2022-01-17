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
        public async Task<ActionResult<DtoOrderReturn>> AddOrder([FromBody] DtoOrderInsert order)
        {
            var result = await _order.Insert(_mapper.Map<Order>(order));
            return Ok(_mapper.Map<DtoOrderReturn>(result));
        }

        [HttpPost("{orderId}/accept")]
        public async Task<ActionResult<DtoOrderReturn>> AcceptOrder(int orderId, [FromBody] DtoOrderAccept order)
        {
            var result = await _order.Update(
                orderId,
                new Order
                {
                    DriverId = order.DriverId,
                    Status = status.accepted
                }
            );
            return Ok(_mapper.Map<DtoOrderReturn>(result));
        }

        [HttpPost("{orderId}/finish")]
        public async Task<ActionResult<DtoOrderReturn>> FinishOrder(int orderId)
        {
            var result = await _order.Update(
                orderId,
                new Order
                {
                    Status = status.finished,
                }
            );
            return Ok(_mapper.Map<DtoOrderReturn>(result));
        }
    }
}