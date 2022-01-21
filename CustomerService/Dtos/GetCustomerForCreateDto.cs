using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class GetCustomerForCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public double Balance { get; set; }
        public string UserId { get; set; }
    }
}