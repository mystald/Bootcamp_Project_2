using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Dto;
using AuthService.Helper;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Data
{
    public class DALUser : IUser
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _um;

        public DALUser(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _um = userManager;
        }
        public Task<string> Authentication(string Username, string Password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return _um.Users.ToList();
        }

        public async Task<ApplicationUser> GetByUserId(int userId)
        {
            var result = await _db.Users.Where(
                user => user.Id == userId.ToString()
            ).SingleOrDefaultAsync();

            if (result == null) throw new Exception("User not found");

            return result;
        }

        public async Task<ApplicationUser> Insert(DtoUserRegister input)
        {
            try
            {
                var newUser = new ApplicationUser
                {
                    UserName = input.Username,
                    PasswordHash = Hash.getHash(input.Password),
                    IsBlocked = false,
                };

                var result = await _um.CreateAsync(newUser);

                if (!result.Succeeded) throw new Exception($"Failed to create user: {result.Errors.ToString()}");

                if (input.Role == role.driver)
                {
                    var newDriver = new DtoDriver
                    {
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                        BirthDate = input.BirthDate,
                        Latitude = 0,
                        Longitude = 0,
                        Balance = 0,
                        IsApprove = false,
                        UserId = newUser.Id
                    };

                    // TODO Send InsertDriver Request to DriverService

                    await _um.AddToRoleAsync(newUser, "Driver");
                }
                else if (input.Role == role.customer)
                {
                    var newCust = new DtoCustomer
                    {
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                        BirthDate = input.BirthDate,
                        Balance = 0,
                        UserId = newUser.Id,
                    };

                    // TODO Send InsertCustomer Request to CustomerService

                    await _um.AddToRoleAsync(newUser, "Customer");
                }

                return newUser;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Task<ApplicationUser> Update(int userId)
        {
            throw new NotImplementedException();
        }
    }
}