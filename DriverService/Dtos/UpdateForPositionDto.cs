using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverService.Dtos
{
    public class UpdateForPositionDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}