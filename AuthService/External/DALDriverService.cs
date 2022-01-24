using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthService.Dto;
using Microsoft.Extensions.Configuration;

namespace AuthService.External
{
    public class DALDriverService : IDriverService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;

        public DALDriverService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _config["ServiceTokens:Admin"]);
        }

        public async Task InsertDriver(DtoDriver driver)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(driver),
                Encoding.UTF8,
                "application/json"
            );

            var url = _config["ExternalServices:Driver"];

            // TODO Generate/AddToken

            var response = await _httpClient.PostAsync(
                $"{url}",
                httpContent
            );

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                throw new System.Exception(response.ToString());
            }
        }
    }
}