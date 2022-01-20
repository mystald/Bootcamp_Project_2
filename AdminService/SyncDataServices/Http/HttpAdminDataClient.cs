using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminService.Dtos;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AdminService.SyncDataServices.Http
{
    public class HttpAdminDataClient : IAdminDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpAdminDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<IEnumerable<DtoCustomerGet>> GetCustomer()
        {
            var url = _configuration["CustomerService"];
            var response = await _httpClient.GetAsync($"{url}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync GET to Customer Service was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync GET to Customer Service failed");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());

            }
            var value = JsonSerializer.Deserialize<IEnumerable<DtoCustomerGet>>(await response.Content.ReadAsByteArrayAsync());
            return value;
        }

        public async Task<IEnumerable<DriverDto>> GetDriver()
        {
            var url = _configuration["DriverService"];
            var response = await _httpClient.GetAsync($"{url}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync GET to Driver Service was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync GET to Driver Service failed");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            var value = JsonSerializer.Deserialize<IEnumerable<DriverDto>>(await response.Content.ReadAsByteArrayAsync());
            return value;
        }

        public async Task<IEnumerable<DtoOrderOutput>> GetOrder()
        {
            var url = _configuration["OrderService"];
            var response = await _httpClient.GetAsync($"{url}/order");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync GET to Order Service was OK !");
            }
            else
            {
                Console.WriteLine("--> Sync GET to Order Service failed");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            var value = JsonSerializer.Deserialize<IEnumerable<DtoOrderOutput>>(await response.Content.ReadAsByteArrayAsync());
            return value;
        }

        public async Task<AcceptDriverReturn> ApproveDriver(AcceptDriverDto insert)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(insert),
                Encoding.UTF8, "application/json");
            var url = _configuration["DriverService"];
            var response = await _httpClient.PutAsync($"{url}/AcceptDriver/{insert.driverId}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PUT to Driver Service was OK !");
                return JsonSerializer.Deserialize<AcceptDriverReturn>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine("--> Sync PUT to Driver Service failed");
                Console.WriteLine(response.StatusCode.ToString());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<DtoPrice> SetPrice(DtoPrice insert)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(insert),
                Encoding.UTF8, "application/json");
            var url = _configuration["OrderService"];
            var response = await _httpClient.PostAsync($"{url}/price", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Order Service was OK !");
                return JsonSerializer.Deserialize<DtoPrice>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Console.WriteLine("--> Sync POST to Order Service failed");
                Console.WriteLine(response.StatusCode.ToString());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}