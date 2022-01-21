using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class GetCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public double Balance { get; set; }
        public string UserId { get; set; }
    }
}