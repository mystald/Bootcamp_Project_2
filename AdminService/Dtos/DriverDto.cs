using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Dtos
{
    public class DriverDto
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public float balance { get; set; }
        public int userId { get; set; }

    }
}