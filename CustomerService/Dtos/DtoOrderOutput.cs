using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class DtoOrderOutput
    {
        public int CustomerId { get; set; }

        public double StartDest { get; set; }
        public double EndDest { get; set; }
        public double Distance { get; set; }
        public double Fee { get; set; }
        public string Status { get; set; }
    }
}