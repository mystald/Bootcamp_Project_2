using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Dtos
{
    public class DriverDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public float balance { get; set; }
        public string userId { get; set; }
        public bool isApprove { get; set; }

    }
}