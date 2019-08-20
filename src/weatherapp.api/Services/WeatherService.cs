using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Domain;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private ICitiesService _citiesService { get; }
        private readonly HttpClient _httpClient;
        private readonly IOptions<WeatherAppConfig> _config;

        public WeatherService(ICitiesService citiesService, HttpClient httpClient, IOptions<WeatherAppConfig> config)
        {
            _citiesService = citiesService;

            httpClient.BaseAddress = new Uri("http://api.openweathermap.org/");
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<AveragedDayForecast>> GetForecast(string cityId, string zipCode)
        {
            string endpoint = GetEndpoint(cityId, zipCode);
            var response = await _httpClient.GetAsync(endpoint);

            var text = await response.Content.ReadAsStringAsync();
            var fiveDayForecast = JsonConvert.DeserializeObject<FiveDayForecast>(text);

            var averagedFiveDayForecast = GetAveragedFiveDayForecast(fiveDayForecast);
            return averagedFiveDayForecast;
        }

        private List<AveragedDayForecast> GetAveragedFiveDayForecast(FiveDayForecast fiveDayForecast)
        {
            var retval = new List<AveragedDayForecast>();
            var daysOfForecast = fiveDayForecast.list.Select(x => DateTime.Parse(x.dt_txt).Date).Distinct();
            foreach (var day in daysOfForecast)
            {
                var dayForecasts = fiveDayForecast.list.Where(x => DateTime.Parse(x.dt_txt).Date == day);
                var averagedDayForecast = new AveragedDayForecast()
                {
                    Date = day,
                    AveragedHumidity = dayForecasts.Average(x => x.main.humidity),
                    AveragedTemperature = dayForecasts.Average(x => x.main.temp),
                    AveragedWind = dayForecasts.Average(x => x.wind.speed)
                };
                retval.Add(averagedDayForecast);
            }

            return retval;
        }

        #region PrivateMethods

        private string GetEndpoint(string cityId, string zipCode)
        {
            var apiKey = _config.Value.OpenWeatherApiKey;
            var endpoint = $"/data/2.5/forecast?APPID={apiKey}";

            if (!string.IsNullOrEmpty(cityId))
            {
                endpoint = endpoint + $"&id={cityId}";
            }

            if (!string.IsNullOrEmpty(zipCode))
            {
                endpoint = endpoint + $"&zip={zipCode},de";
            }

            return endpoint;
        }
        #endregion
    }
}
