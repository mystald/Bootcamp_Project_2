using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Dtos
{
    public class DtoCustomerGet
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime birthDate { get; set; }
        public double balance { get; set; }
        public string userId { get; set; }
    }
}