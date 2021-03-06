using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;
using AuthService.Dto;
using AuthService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
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

        [HttpPost("Authentication")]
        public async Task<ActionResult<ApplicationUser>> Authentication([FromBody] DtoUserCredentials credentials)
        {
            try
            {
                var user = await _user.Authentication(credentials.Username, credentials.Password);

                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{userId}/{status}")]
        public async Task<ActionResult<ApplicationUser>> UpdateUserBlockStatus(string userId, string status)
        {
            try
            {
                if (status != Dto.status.block.ToString() && status != Dto.status.unblock.ToString())
                {
                    return base.NotFound();
                }

                var result = await _user.Update(
                    userId,
                    new ApplicationUser
                    {
                        IsBlocked = status.Equals(Dto.status.block.ToString())
                    }
                );

                return Ok(result.IsBlocked);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("servicetoken/{role}")]
        public ActionResult<string> GetServiceToken(string role)
        {
            try
            {
                return Ok(
                    _user.GenerateServiceToken(role)
                );
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}