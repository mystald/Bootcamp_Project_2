using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Dto;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Data
{
    public interface IUser
    {
        IEnumerable<ApplicationUser> GetAllUser();
        Task<ApplicationUser> GetByUserId(int userId);
        Task<ApplicationUser> Insert(DtoUserRegister input);
        Task<ApplicationUser> Update(int userId);
        Task<string> Authentication(string Username, string Password);
    }
}