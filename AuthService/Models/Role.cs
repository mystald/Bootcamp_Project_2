using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}