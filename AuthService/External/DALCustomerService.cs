using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AuthService.Dto;
using Microsoft.Extensions.Configuration;

namespace AuthService.External
{
    public class DALCustomerService : ICustomerService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;

        public DALCustomerService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task InsertCustomer(DtoCustomer customer)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(customer),
                Encoding.UTF8,
                "application/json"
            );

            var url = _config["ExternalServices:Customer"];

            // TODO Generate/AddToken

            var response = await _httpClient.PostAsync(
                $"{url}",
                httpContent
            );

            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception(response.ToString());
            }
        }
    }
}