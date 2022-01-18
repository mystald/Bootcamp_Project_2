using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class DtoFeeOutput
    {
        public location startDest { get; set; }
        public location endDest { get; set; }
        public double fee { get; set; }
    }
}