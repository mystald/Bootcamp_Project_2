using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerService.Dtos;
using Microsoft.Extensions.Configuration;

namespace CustomerService.SyncDataServices.Http
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

        public async Task<DtoFee> CheckFee(int CustomerId)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(CustomerId),
                Encoding.UTF8, "application/json");
            var url = _configuration["OrderService"];
            var response = await _httpClient.PostAsync($"{url}/fee", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Order Service was OK !");
            }
            else
            {
                Console.WriteLine(response.Content.ToString());
                Console.WriteLine("--> Sync POST to Order Service failed");
            }
            var value = JsonSerializer.Deserialize<DtoFee>(await response.Content.ReadAsStringAsync());
            return value;

        }

        public async Task CreateOrder(DtoOrderInsert ins)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(ins),
                Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync(_configuration["AppSettings:OrderService"],
                httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Order Service was OK !");
            }
            else
            {
                Console.WriteLine(response.Content.ToString());
                Console.WriteLine(response.StatusCode.ToString());
                Console.WriteLine("--> Sync POST to Order Service failed");
            }
        }

        public async Task<DtoOrderOutput> GetOrderHistory(int CustomerId)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(CustomerId),
                Encoding.UTF8, "application/json");
            var url = _configuration["OrderService"];
            var response = await _httpClient.PostAsync($"{url}/history", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Order Service was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync POST to Order Service failed");
            }
            var value = JsonSerializer.Deserialize<DtoOrderOutput>(await response.Content.ReadAsStringAsync());
            return value;
        }
    }
}

//ni kalo ke local-->"PaymentService":"http://localhost:24183/api/p/enrollments",

