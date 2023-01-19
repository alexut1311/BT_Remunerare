using BT_Remunerare.TL.Common;
using BT_Remunerare.TL.DTO;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BT_Remunerare.IntegrationTests.Controllers
{
    public class SaleControllerUnitTests : IClassFixture<Config>
    {
        private readonly HttpClient httpClient;
        public SaleControllerUnitTests(Config config)
        {
            httpClient = config.CreateClient();
        }

        [Fact]
        public async Task AddSale_IsExecuted()
        {
            SaleDTO saleDTO = new SaleDTO
            {
                ProductId = 1,
                PeriodId = 1,
                NumberOfProducts = 1,
                VendorId = 1
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(saleDTO), Encoding.UTF8, "application/json");

            using var httpResponse = await httpClient.PostAsync("/api/Sale/AddSale", stringContent);

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<Response>(stringResponse);

            response.IsSuccesful.Should().BeTrue();
        }
    }
}
