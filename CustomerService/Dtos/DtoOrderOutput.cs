using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class DtoOrderOutput
    {
        public int customerId { get; set; }
        public int? driverId { get; set; }
        public location startDest { get; set; }
        public location endDest { get; set; }
        public double distance { get; set; }
        public double fee { get; set; }
        public string status { get; set; }
    }
}