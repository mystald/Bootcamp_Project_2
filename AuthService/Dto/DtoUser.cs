using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Dto
{
    public class DtoUserRegisterDriver
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DtoUserRegisterCustomer
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DtoUserRegister
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public role Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class DtoUserOutput
    {

    }

    public enum role
    {
        driver,
        customer,
    }
}