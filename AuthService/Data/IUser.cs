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
        Task<string> Authentication(string username, string password);
        string GenerateServiceToken(string role);
        IEnumerable<ApplicationUser> GetAllUser();
        Task<ApplicationUser> GetByUserId(string userId);
        Task<IList<string>> GetRolesByUsername(string username);
        Task<ApplicationUser> Insert(DtoUserRegister input);
        Task<ApplicationUser> Update(string userId, ApplicationUser userObj);
    }
}