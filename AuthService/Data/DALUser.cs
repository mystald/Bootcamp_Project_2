using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthService.Dto;
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

        public DALUser(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IConfiguration config
        )
        {
            _db = db;
            _um = userManager;
            _config = config;
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