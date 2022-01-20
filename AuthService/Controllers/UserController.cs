using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;
using AuthService.Dto;
using AuthService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUser _user;
        private IMapper _mapper;

        public UserController(IUser user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ApplicationUser> GetAllUser()
        {
            var result = _user.GetAllUser();
            return Ok(result);
        }

        [HttpPost("Registration/Driver")]
        public async Task<ActionResult<ApplicationUser>> RegistrationDriver(DtoUserRegisterDriver input)
        {
            try
            {
                var result = await _user.Insert(_mapper.Map<DtoUserRegister>(input));
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Registration/Customer")]
        public async Task<ActionResult<ApplicationUser>> RegistrationCustomer(DtoUserRegisterCustomer input)
        {
            try
            {
                var result = await _user.Insert(_mapper.Map<DtoUserRegister>(input));
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}