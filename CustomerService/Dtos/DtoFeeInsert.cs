using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class DtoFeeInsert
    {
        public location StartDest { get; set; }
        public location EndDest { get; set; }
    }
}