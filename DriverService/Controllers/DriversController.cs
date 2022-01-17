using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DriverService.Data;
using DriverService.Dtos;
using DriverService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DriverService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : ControllerBase
    {
        private IDriver _driver;
        private IMapper _mapper;
        public DriversController(IDriver driver, IMapper mapper)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        //Get All 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll()
        {
            var drivers = await _driver.GetAll();
            var dtos = _mapper.Map<IEnumerable<DriverDto>>(drivers);
            return Ok(dtos);
        }

        //Get Balance Driver By Id
        [HttpGet("GetBalance/{id}")]
        public async Task<ActionResult<GetDriverBalanceDto>> GetBalance(int id)
        {
            var result = await _driver.GetBalanceById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<GetDriverBalanceDto>(result));
        }

        //Get Profile Driver By Id
        [HttpGet("GetProfile/{id}")]
        public async Task<ActionResult<GetDriverProfileDto>> GetProfile(int id)
        {
            var result = await _driver.GetProfileById(id.ToString());
            if (result == null)
                return NotFound();

            return Ok(_mapper.Map<GetDriverProfileDto>(result));
        }

        //Update Position Driver By Id
        [HttpPut("UpdatePosition/{id}")]
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
    }
}