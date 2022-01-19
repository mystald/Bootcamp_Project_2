using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Data
{
    public interface IPrice
    {
        Task<Price> Update(string name, Price obj);
        double GetByName(string name);
    }
}