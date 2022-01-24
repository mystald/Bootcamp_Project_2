using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CustomerService.Dtos;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http.Headers;

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

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _configuration["ServiceTokens:Customer"]);
        }

        public async Task<DtoFeeOutput> CheckFee(DtoFeeInsert insert)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(insert),
                Encoding.UTF8, "application/json");
            var url = _configuration["AppSettings:OrderService"];
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
            Console.WriteLine(response.Content.ReadAsStringAsync());
            var value = JsonSerializer.Deserialize<DtoFeeOutput>(await response.Content.ReadAsStringAsync());
            return value;

        }

        public async Task<DtoOrderOutput> CreateOrder(DtoOrderInsert ins)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(ins),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                _configuration["AppSettings:OrderService"],
                httpContent
            );

            // TODO delete trace logs
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Order Service was OK !");
                return JsonSerializer.Deserialize<DtoOrderOutput>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine("--> Sync POST to Order Service failed");
                Console.WriteLine(response.StatusCode.ToString());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<IEnumerable<DtoOrderOutput>> GetOrderHistory(int CustomerId)
        {
            // var httpContent = new StringContent(
            //     JsonSerializer.Serialize(CustomerId),
            //     Encoding.UTF8, "application/json");
            var url = _configuration["AppSettings:OrderService"];
            var response = await _httpClient.GetAsync($"{url}/customer/{CustomerId}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Order Service was OK !");
                var value = JsonSerializer.Deserialize<IEnumerable<DtoOrderOutput>>(await response.Content.ReadAsByteArrayAsync());
                return value;
            }
            else
            {
                Console.WriteLine("--> Sync POST to Order Service failed");
                Console.WriteLine(response.StatusCode);
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}

//ni kalo ke local-->"PaymentService":"http://localhost:24183/api/p/enrollments",

