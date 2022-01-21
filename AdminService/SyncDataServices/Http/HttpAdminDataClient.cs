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
                return JsonSerializer.Deserialize<IEnumerable<DtoCustomerGet>>(await response.Content.ReadAsByteArrayAsync());
            }
            else
            {
                Console.WriteLine("--> Sync GET to Customer Service failed");
                Console.WriteLine(response.StatusCode);
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
        }

        public async Task<DtoCustomerGet> GetCustomerById(int id)
        {
            var url = _configuration["CustomerService"];
            var response = await _httpClient.GetAsync($"{url}/{id}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync GET to Customer Service was OK !");
                return JsonSerializer.Deserialize<DtoCustomerGet>(await response.Content.ReadAsByteArrayAsync());
            }
            else
            {
                Console.WriteLine("--> Sync GET to Customer Service failed");
                Console.WriteLine(response.StatusCode);
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
        }

        public async Task<IEnumerable<DriverDto>> GetDriver()
        {
            var url = _configuration["DriverService"];
            var response = await _httpClient.GetAsync($"{url}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync GET to Driver Service was OK !");
                return JsonSerializer.Deserialize<IEnumerable<DriverDto>>(await response.Content.ReadAsByteArrayAsync());
            }
            else
            {
                Console.WriteLine("--> Sync GET to Driver Service failed");
                Console.WriteLine(response.StatusCode);
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<DriverDto> GetDriverById(int id)
        {
            var url = _configuration["DriverService"];
            var response = await _httpClient.GetAsync($"{url}/{id}");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync GET to Driver Service was OK !");
                return JsonSerializer.Deserialize<DriverDto>(await response.Content.ReadAsByteArrayAsync());
            }
            else
            {
                Console.WriteLine("--> Sync GET to Driver Service failed");
                Console.WriteLine(response.StatusCode);
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
        }

        public async Task<IEnumerable<DtoOrderOutput>> GetOrder()
        {
            var url = _configuration["OrderService"];
            var response = await _httpClient.GetAsync($"{url}/order");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync GET to Order Service was OK !");
                return JsonSerializer.Deserialize<IEnumerable<DtoOrderOutput>>(await response.Content.ReadAsByteArrayAsync());
            }
            else
            {
                Console.WriteLine("--> Sync GET to Order Service failed");
                Console.WriteLine(response.StatusCode);
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<AcceptDriverReturn> ApproveDriver(AcceptDriverDto insert)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(insert),
                Encoding.UTF8, "application/json");
            var url = _configuration["DriverService"];
            var response = await _httpClient.PatchAsync($"{url}/{insert.driverId}/Approve", httpContent);
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

        public async Task<string> BlockCustomer(string userId)
        {
            var httpContent = new StringContent(
                "",
                Encoding.UTF8, "application/json");
            var url = _configuration["AuthService"];
            var response = await _httpClient.PatchAsync($"{url}/{userId}/block", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PATCH to Auth Service was OK !");
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine("--> Sync PATCH to Auth Service failed");
                Console.WriteLine(response.StatusCode.ToString());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<string> UnblockCustomer(string userId)
        {
            var httpContent = new StringContent(
                "",
                Encoding.UTF8, "application/json");
            var url = _configuration["AuthService"];
            var response = await _httpClient.PatchAsync($"{url}/{userId}/unblock", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PATCH to Auth Service was OK !");
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine("--> Sync PATCH to Auth Service failed");
                Console.WriteLine(response.StatusCode.ToString());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<string> BlockDriver(string userId)
        {
            var httpContent = new StringContent(
                "",
                Encoding.UTF8, "application/json");
            var url = _configuration["AuthService"];
            var response = await _httpClient.PatchAsync($"{url}/{userId}/block", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PATCH to Auth Service was OK !");
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine("--> Sync PATCH to Auth Service failed");
                Console.WriteLine(response.StatusCode.ToString());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<string> UnblockDriver(string userId)
        {
            var httpContent = new StringContent(
                "",
                Encoding.UTF8, "application/json");
            var url = _configuration["AuthService"];
            var response = await _httpClient.PatchAsync($"{url}/{userId}/unblock", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync PATCH to Auth Service was OK !");
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine("--> Sync PATCH to Auth Service failed");
                Console.WriteLine(response.StatusCode.ToString());
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }
    }
}