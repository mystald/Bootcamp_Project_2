using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverService.Dtos
{
    public class FinishOrderDto
    {
        public int OrderId {get; set;}
        public int DriverId { get; set; }
    }
}