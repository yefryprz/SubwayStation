using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SubwayStation.Domain.DTOs;
using SubwayStation.Domain.Models;
using SubwayStation.Infrastructure.ConfigInjections;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SubwayStation.Test
{
    public class SubwayStationTest
    {
        private readonly CustomEndPointTest _customEndPointTest;

        public SubwayStationTest()
        {
            //Inject dependencies to run server work
            _customEndPointTest = new CustomEndPointTest(x =>
            {
                x.AddAutoMapperConfig();
                x.AddRepositoryInjections();
                x.AddServicesInjections();
                x.AddControllers();
            });
        }

        [Fact]
        public async void GetSubways()
        {
            var token = await GetToken();
            var headerEntry = new AuthenticationHeaderValue("Bearer", token);

            using var httpClient = _customEndPointTest.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = headerEntry;
            var response = await httpClient.GetAsync("/api/Subways");

            var responseText = await response.Content.ReadFromJsonAsync<PageListDTO<SubwayDTO>>();

            Assert.NotEmpty(responseText.ResultList);
        }

        [Fact]
        public async void GetFrequently()
        {
            var token = await GetToken();
            var headerEntry = new AuthenticationHeaderValue("Bearer", token);

            using var httpClient = _customEndPointTest.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = headerEntry;
            var response = await httpClient.GetAsync("/api/Subways/GetFrequently");

            var responseText = await response.Content.ReadFromJsonAsync<PageListDTO<FrequentlyDTO>>();

            Assert.NotEmpty(responseText.ResultList);
        }

        [Fact]
        public async void CalculateDistances()
        {
            var token = await GetToken();
            var headerEntry = new AuthenticationHeaderValue("Bearer", token);

            using var httpClient = _customEndPointTest.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = headerEntry;


            var loginParams = new Dictionary<string, dynamic>
            {
               { "objectIdFrom", "2201" }, //replace subway id value
               { "objectIdTo", "2203" }, //replace subway id value
            };

            var reqParams = JsonConvert.SerializeObject(loginParams);
            var plainString = new StringContent(reqParams, System.Text.Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/api/Subways/Distances", plainString);
            var responseText = await response.Content.ReadFromJsonAsync<ResponseModel>();

            Assert.NotNull(responseText.Message);
        }

        private async Task<string> GetToken()
        {
            using var httpClient = _customEndPointTest.CreateClient();

            var loginParams = new Dictionary<string, dynamic>
            {
                { "userName", "yefryprz" }, //put userName
                { "password", "Perez*025" }, //put password
            };

            var reqParams = JsonConvert.SerializeObject(loginParams);
            var plainString = new StringContent(reqParams, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/account/login", plainString);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadFromJsonAsync<ResponseModel<TokenDTO>>();
                return responseContent.Result.AccessToken;
            }

            return "Invalid Credentials";
        }
    }

    //Config to run backend code in background while run test and call endpoint directly
    internal class CustomEndPointTest : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection> _serviceCollection;

        public CustomEndPointTest(Action<IServiceCollection> serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(_serviceCollection);
            return base.CreateHost(builder);
        }
    }
}