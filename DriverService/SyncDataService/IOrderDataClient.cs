using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverService.Dtos;

namespace DriverService.SyncDataService.Http
{
    public interface IOrderDataClient
    {
        Task SendDriverToOderForAccept (AcceptOrderDto dri);
        Task SendDriverToOderForFinish (FinishOrderDto dri);
    }
}