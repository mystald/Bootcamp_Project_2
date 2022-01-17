using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Dto
{
    public class DtoOrderInsert
    {
        public int CustomerId { get; set; }
        public location startDest { get; set; }
        public location endDest { get; set; }
    }

    public class DtoOrderReturn
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int? DriverId { get; set; }
        public location StartDest { get; set; }
        public location EndDest { get; set; }
        public double Distance { get; set; }
        public double Fee { get; set; }
        public string Status { get; set; }
    }

    public class DtoOrderAccept
    {
        public int DriverId { get; set; }
    }

    public class location
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}