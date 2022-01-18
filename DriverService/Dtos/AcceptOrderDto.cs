using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverService.Dtos
{
    public class AcceptOrderDto
    {
        public int OrderId { get; set; } 
        public int DriverId { get; set; }
        public double DriverLat { get; set; }
        public double DriverLong { get; set; } 
    }
}