using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Domain;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private ICitiesService _citiesService { get; }
        private readonly IHttpClientFactory _clientFactory;

        public WeatherService(ICitiesService citiesService, IHttpClientFactory clientFactory)
        {
            _citiesService = citiesService;
            _clientFactory = clientFactory;
        }

        public async Task<string> GetForecast(string cityId, string zipCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            @"http://api.openweathermap.org/data/2.5/forecast?id=6548737&APPID=fcadd28326c90c3262054e0e6ca599cd");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            var text = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                
            }
            else
            {
                
            }

            return null;
        }

    }
}
