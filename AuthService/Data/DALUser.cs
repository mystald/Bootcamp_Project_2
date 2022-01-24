using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthService.Dto;
using AuthService.External;
using AuthService.Helper;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Data
{
    public class DALUser : IUser
    {
        private ApplicationDbContext _db;
        private UserManager<ApplicationUser> _um;
        private IConfiguration _config;
        private ICustomerService _customer;
        private IDriverService _driver;

        public DALUser(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            ICustomerService customer,
            IDriverService driver
        )
        {
            _db = db;
            _um = userManager;
            _config = config;
            _customer = customer;
            _driver = driver;
        }
        public async Task<string> Authentication(string username, string password)
        {
            var user = await _um.FindByNameAsync(username);
            if (user == null) throw new Exception("Invalid Credentials");

            var userFound = await _um.CheckPasswordAsync(
                user,
                password
            );
            if (!userFound) throw new Exception("Invalid Credentials");

            if (user.IsBlocked) throw new Exception("User blocked");

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));

            var roles = await GetRolesByUsername(username);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            user.Token = tokenString;

            return tokenString;
        }

        public string GenerateServiceToken(string Role)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "Services"));
            claims.Add(new Claim(ClaimTypes.Role, Role));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMonths(6),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return _um.Users.ToList();
        }

        public async Task<ApplicationUser> GetByUserId(string userId)
        {
            var result = await _um.FindByIdAsync(userId);

            if (result == null) throw new Exception("User not found");

            return result;
        }

        public async Task<IList<string>> GetRolesByUsername(string username)
        {
            var user = await _um.FindByNameAsync(username);

            var roles = await _um.GetRolesAsync(user);

            return roles;
        }

        public async Task<ApplicationUser> Insert(DtoUserRegister input)
        {
            try
            {
                var newUser = new ApplicationUser
                {
                    UserName = input.Username,
                    IsBlocked = false,
                };

                var result = await _um.CreateAsync(newUser, input.Password);

                if (!result.Succeeded) throw new Exception($"Failed to create user: {result.ToString()}");

                if (input.Role == role.driver)
                {
                    try
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

                        await _driver.InsertDriver(newDriver);

                        await _um.AddToRoleAsync(newUser, "Driver");
                    }
                    catch (System.Exception)
                    {
                        await _um.DeleteAsync(newUser);
                        throw;
                    }
                }
                else if (input.Role == role.customer)
                {
                    try
                    {
                        var newCust = new DtoCustomer
                        {
                            FirstName = input.FirstName,
                            LastName = input.LastName,
                            BirthDate = input.BirthDate,
                            Balance = 0,
                            UserId = newUser.Id,
                        };

                        await _customer.InsertCustomer(newCust);

                        await _um.AddToRoleAsync(newUser, "Customer");
                    }
                    catch (System.Exception)
                    {
                        await _um.DeleteAsync(newUser);
                        throw;
                    }
                }

                return newUser;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<ApplicationUser> Update(string userId, ApplicationUser userObj)
        {
            try
            {
                var oldUser = await GetByUserId(userId);

                if (userObj.UserName != null)
                {
                    oldUser.UserName = userObj.UserName;
                    oldUser.PasswordHash = userObj.PasswordHash;
                }
                else
                {
                    oldUser.IsBlocked = userObj.IsBlocked;
                }

                var result = await _um.UpdateAsync(oldUser);
                if (!result.Succeeded) throw new Exception(result.ToString());

                return oldUser;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}