using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverService.Dtos
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public float Balance { get; set; }
        public bool IsApprove { get; set; }
        public string UserId { get; set; }
    }
}