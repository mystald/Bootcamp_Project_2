using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using OrderService.Data;
using OrderService.Dto;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private IOrder _order;
        private IMapper _mapper;

        public OrderController(IOrder order, IMapper mapper)
        {
            _order = order;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DtoOrderReturn>>> GetAllOrder()
        {
            var result = await _order.GetAll();
            return Ok(_mapper.Map<IEnumerable<DtoOrderReturn>>(result));
        }

        [HttpPost("fee")]
        public ActionResult<DtoOrderCheckFeeOutput> GetFee(DtoOrderCheckFeeInsert input)
        {
            var inputPoint = _mapper.Map<Order>(input);
            var result = _order.GetFeeByDistance(inputPoint.StartDest.Distance(inputPoint.EndDest));
            return Ok(new DtoOrderCheckFeeOutput
            {
                StartDest = input.StartDest,
                EndDest = input.EndDest,
                Fee = result,
            });
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<DtoOrderReturn>>> GetByCustomer(int customerId)
        {
            var result = await _order.GetByCustomerId(customerId);
            return Ok(_mapper.Map<IEnumerable<DtoOrderReturn>>(result));
        }

        [HttpPost]
        public async Task<ActionResult<DtoOrderReturn>> AddOrder([FromBody] DtoOrderInsert order)
        {
            try
            {
                var result = await _order.Insert(_mapper.Map<Order>(order));
                return Ok(_mapper.Map<DtoOrderReturn>(result));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{orderId}/accept")]
        public async Task<ActionResult<DtoOrderReturn>> AcceptOrder(int orderId, [FromBody] DtoOrderAccept driver)
        {
            try
            {
                var order = await _order.GetById(orderId);

                var driverPosition = new Point(driver.DriverLat, driver.DriverLong) { SRID = 4326 };

                if (order.Status == status.accepted || order.Status == status.finished) return BadRequest("Order is not available anymore");

                if ((order.StartDest.Distance(driverPosition)) / 1000 > 5) return BadRequest("Driver Position > 5 KM from Starting Point");

                var result = await _order.Update(
                    orderId,
                    new Order
                    {
                        DriverId = driver.DriverId,
                        Status = status.accepted
                    }
                );
                return Ok(_mapper.Map<DtoOrderReturn>(result));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{orderId}/finish")]
        public async Task<ActionResult<DtoOrderReturn>> FinishOrder(int orderId, [FromBody] DtoOrderFinish driver)
        {
            try
            {
                var order = await _order.GetById(orderId);

                if (order.DriverId != driver.DriverId) return BadRequest("Invalid DriverID");

                var result = await _order.Update(
                    orderId,
                    new Order
                    {
                        Status = status.finished,
                    }
                );
                return Ok(_mapper.Map<DtoOrderReturn>(result));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}