using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Dto
{
    public class DtoOrderInsert
    {
        public int CustomerId { get; set; }
        public location startDest { get; set; }
        public location endDest { get; set; }
    }

    public class location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}