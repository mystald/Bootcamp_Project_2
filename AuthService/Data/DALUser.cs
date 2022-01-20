using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Dto;
using AuthService.Helper;
using AuthService.Models;

namespace AuthService.Data
{
    public class DALUser : IUser
    {
        private ApplicationDbContext _db;

        public DALUser(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<string> Authentication(string Username, string Password)
        {
            throw new NotImplementedException();
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

                var addedUser = await _db.Users.AddAsync(newUser);

                await _db.SaveChangesAsync();

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
                        UserId = addedUser.Entity.Id,
                    };

                    // TODO Send InsertDriver Request to DriverService
                }
                else if (input.Role == role.customer)
                {
                    var newCust = new DtoCustomer
                    {
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                        BirthDate = input.BirthDate,
                        Balance = 0,
                        UserId = addedUser.Entity.Id,
                    };

                    // TODO Send InsertCustomer Request to CustomerService
                }

                return addedUser.Entity;
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