using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverService.Dtos
{
    public class OrderDto
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public int? driverId { get; set; }
        public location startDest { get; set; }
        public location endDest { get; set; }
        public double distance { get; set; }
        public double fee { get; set; }
        public string status { get; set; }   
    }

     public class location
    {
        public double lat { get; set; }
        public double @long { get; set; }
    }
}