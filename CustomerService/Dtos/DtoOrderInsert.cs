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
        public double startDest { get; set; }
        public double endDest { get; set; }
    }


}