using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DriverService.Dtos;
using DriverService.Models;
using Microsoft.Extensions.Configuration;

namespace DriverService.SyncDataService.Http
{
    public class HttpOrderDataClient : IOrderDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpOrderDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendDriverToOderForAccept(AcceptOrderDto dri)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(dri),
                Encoding.UTF8, "application/json");

            var url = _configuration["LocalOrderService"];
            
            var response = await _httpClient.PostAsync($"{url}/{dri.DriverId}/accept", httpContent);

            if (response.IsSuccessStatusCode) 
            {
                Console.WriteLine("--> Sync POST to OrderService Was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to OrderService Failed");
            }
        }

        public async Task SendDriverToOderForFinish(FinishOrderDto dri)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(dri),
                Encoding.UTF8, "application/json");

            var url = _configuration["LocalOrderService"];

            var response = await _httpClient.PostAsync($"{url}/{dri.DriverId}/finish", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to OrderService Was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to PaymentService Failed");
            }
        }

    }
}