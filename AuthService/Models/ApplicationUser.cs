using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsBlocked { get; set; } // TODO Set default as False
        public string Token { get; set; }
    }
}