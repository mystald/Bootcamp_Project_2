using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public double Balance { get; set; }
        
        [Required]
        public int? UserId {get; set;}

        [Required]
        public double StartDest { get; set; }
        
        [Required]
        public double EndDest { get; set; }

        
    }
}