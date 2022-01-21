using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DriverService.Data;
using DriverService.Dtos;
using DriverService.Models;
using DriverService.SyncDataService.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private IDriver _driver;
        private IMapper _mapper;
        private readonly IOrderDataClient _orderDataClient;
        public DriversController(IDriver driver, IMapper mapper, IOrderDataClient orderDataClient)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _orderDataClient = orderDataClient;
        }

        //Get All 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll()
        {
            var drivers = await _driver.GetAll();
            var dtos = _mapper.Map<IEnumerable<DriverDto>>(drivers);
            return Ok(dtos);
        }

         //Get Driver By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<DriverDto>> GetDriverById(int id)
        {
            var result = await _driver.GetById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<DriverDto>(result));
        }

        //Get Balance Driver By Id
        [HttpGet("{id}/Balance")]
        public async Task<ActionResult<GetDriverBalanceDto>> GetBalance(int id)
        {
            var result = await _driver.GetBalanceById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<GetDriverBalanceDto>(result));
        }

        //Get Profile Driver By Id
        [HttpGet("{id}/Profile")]
        public async Task<ActionResult<GetDriverProfileDto>> GetProfile(int id)
        {
            var result = await _driver.GetProfileById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<GetDriverProfileDto>(result));
        }

        //Update Position Driver By Id
        [HttpPut("{id}/Position")]
        public ActionResult<DriverDto> UpdatePosition(int id, UpdateForPositionDto updateForPositionDto)
        {
            try
            {
                var driverModel = _mapper.Map<Driver>(updateForPositionDto);
                _driver.UpdatePosition(id, driverModel);
                _driver.SaveChanges();

                if (updateForPositionDto != null)
                {
                    return Ok(updateForPositionDto);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Accept Driver By Id
        [HttpPatch("{id}/Approve")]
        public ActionResult<DriverDto> AcceptDriver(int id, AcceptDriverDto acceptDriverDto)
        {
            try
            {
                var driverModel = _mapper.Map<Driver>(acceptDriverDto);
                _driver.AcceptDriver(id, driverModel);
                _driver.SaveChanges();

                if (acceptDriverDto != null)
                {
                    return Ok(acceptDriverDto);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update Accept Order
        [HttpPost("Order/Accept")]
        public async Task<ActionResult<AcceptOrderDto>> UpdateAcceptOrder(AcceptOrderDto acceptOrderDto)
        {
            try
            {
                var driverGetById = await _driver.GetProfileById(acceptOrderDto.DriverId.ToString());
                
                if (driverGetById != null)
                {
                    if(driverGetById.IsApprove == false) return BadRequest("Driver belum di approve");
                    //send sync communication
                    try
                    {
                        await _orderDataClient.SendDriverToOderForAccept(acceptOrderDto);
                        return Ok(acceptOrderDto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"--> Could Not Send Synchronously: {ex.Message}");
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update Finish Order
        [HttpPost("Order/Finish")]
        public async Task<ActionResult<AcceptOrderDto>> FinishAcceptOrder(FinishOrderDto finishOrderDto)
        {
            try
            {
                var driverGetById = await _driver.GetProfileById(finishOrderDto.DriverId.ToString());

                if (driverGetById != null)
                {
                    if(driverGetById.IsApprove == false) return BadRequest("Driver belum di approve");
                    //send sync communication
                    try
                    {
                        await _orderDataClient.SendDriverToOderForFinish(finishOrderDto);
                        return Ok(finishOrderDto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"--> Could Not Send Synchronously: {ex.Message}");
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Insert Driver
        [HttpPost]
        public async Task<ActionResult<DriverDto>> InsertDriver([FromBody] DriverInsertDto input)
        {
            try
            {
                var result = await _driver.Insert(_mapper.Map<Driver>(input));

                return Ok(_mapper.Map<DriverDto>(result));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}