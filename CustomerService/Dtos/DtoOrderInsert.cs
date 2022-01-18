using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace CustomerService.Dtos
{
    public class DtoOrderInsert
    {
        public int CustomerId { get; set; }
        public location startDest { get; set; }
        public location endDest { get; set; }
    }

    public class location
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }


}