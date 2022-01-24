using System;
using System.Net.Http;
using System.Net.Http.Headers;
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

            httpClient.DefaultRequestHeaders.Authorization =
                 new AuthenticationHeaderValue("Bearer", _configuration["ServiceTokens:Driver"]);
        }

        public async Task<OrderDto> SendDriverToOderForAccept(AcceptOrderDto dri)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(dri),
                Encoding.UTF8, "application/json");

            var url = _configuration["OrderService"];

            var response = await _httpClient.PostAsync($"{url}/{dri.OrderId}/accept", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to OrderService Was OK !");
                 return JsonSerializer.Deserialize<OrderDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine(response.Content.ToString());
                Console.WriteLine("--> Sync POST to OrderService Failed");
                throw new Exception(response.ToString());
            }
        }

        public async Task<OrderDto> SendDriverToOderForFinish(FinishOrderDto dri)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(dri),
                Encoding.UTF8, "application/json");

            var url = _configuration["OrderService"];

            var response = await _httpClient.PostAsync($"{url}/{dri.OrderId}/finish", httpContent);

            if (response.IsSuccessStatusCode)
            {
                
                Console.WriteLine("--> Sync POST to OrderService Was OK !");
                return JsonSerializer.Deserialize<OrderDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine("--> Sync POST to PaymentService Failed");
                throw new Exception(response.ToString());
            }
        }

    }
}