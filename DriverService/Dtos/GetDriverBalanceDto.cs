using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverService.Dtos
{
    public class GetDriverBalanceDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Balance { get; set; }
    }
}