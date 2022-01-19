using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Dtos
{
    public class DtoOrderOutput
    {
        public int id { get; set; }
        public int customerId { get; set; }
        public int? driverId { get; set; }
        public locationOutput startDest { get; set; }
        public locationOutput endDest { get; set; }
        public double distance { get; set; }
        public double fee { get; set; }
        public string status { get; set; }
    }

    public class DtoOrderForKafka
    {
        public int OrderId { get; set; }
        public locationOutput StartingPoint { get; set; }
        public locationOutput Destination { get; set; }
        public double Distance { get; set; }
        public int Fee { get; set; }
    }

    public class locationOutput
    {
        public double lat { get; set; }
        public double @long { get; set; }
    }
}