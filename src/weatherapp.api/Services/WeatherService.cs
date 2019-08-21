using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Data;
using WeatherApp.Domain;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private ICitiesService _citiesService { get; }
        private readonly HttpClient _httpClient;
        private IMemoryCache _cache;
        private readonly IOptions<WeatherAppConfig> _config;
        private readonly WeatherAppDbContext _dbContext;

        private string WeatherCache { get { return "_WeatherCache-CityId={0}-zipCode={1}"; } }

        public WeatherService(ICitiesService citiesService, HttpClient httpClient, IOptions<WeatherAppConfig> config, IMemoryCache memoryCache, WeatherAppDbContext dbContext)
        {
            _citiesService = citiesService;

            httpClient.BaseAddress = new Uri("http://api.openweathermap.org/");
            _httpClient = httpClient;
            _config = config;

            _cache = memoryCache;
            _dbContext = dbContext;
        }

        public async Task<List<AveragedDayForecastModel>> GetForecast(string cityId, string zipCode)
        {
            //OpenWeatherMap: We recommend making calls to the API no more than one time every 10 minutes for one location
            //let's cache the response for 10 minutes
            List<AveragedDayForecastModel> averagedFiveDayForecast;
            if (!_cache.TryGetValue(String.Format(WeatherCache, cityId, zipCode), out averagedFiveDayForecast))
            {
                string endpoint = GetEndpoint(cityId, zipCode);
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var text = await response.Content.ReadAsStringAsync();
                    var fiveDayForecast = JsonConvert.DeserializeObject<FiveDayForecast>(text);

                    averagedFiveDayForecast = GetAveragedFiveDayForecast(fiveDayForecast);
                    _cache.Set(string.Format(WeatherCache, cityId, zipCode), averagedFiveDayForecast, TimeSpan.FromMinutes(10));
                }
                else
                {
                    return null;
                }

            }

            return averagedFiveDayForecast;
        }


        public async Task<CityForecast> CreateCityForecast(string cityName, DateTime date, double humidity, double temperature)
        {
            var cityForecast = new CityForecast()
            {
                CityName = cityName,
                Date = date,
                Humidity = humidity,
                Temperature = temperature
            };

            await _dbContext.CityForecasts.AddAsync(cityForecast);
            await _dbContext.SaveChangesAsync();

            return cityForecast;
        }


        public async Task<List<CityForecast>> GetForecastHistory()
        {
            var history = await _dbContext.CityForecasts.ToListAsync();
            return history;
        }
        #region PrivateMethods


        private List<AveragedDayForecastModel> GetAveragedFiveDayForecast(FiveDayForecast fiveDayForecast)
        {
            var retval = new List<AveragedDayForecastModel>();
            var daysOfForecast = fiveDayForecast.DailyForecastList.Select(x => DateTime.Parse(x.DateTimeText).Date).Distinct();
            foreach (var day in daysOfForecast)
            {
                var dayForecasts = fiveDayForecast.DailyForecastList.Where(x => DateTime.Parse(x.DateTimeText).Date == day);
                var averagedDayForecast = new AveragedDayForecastModel()
                {
                    Date = day,
                    AveragedHumidity = dayForecasts.Average(x => x.WeatherConditions.Humidity),
                    AveragedTemperature = dayForecasts.Average(x => x.WeatherConditions.Temperature),
                    AveragedWind = dayForecasts.Average(x => x.WindStats.Speed)
                };
                retval.Add(averagedDayForecast);
            }

            return retval;
        }


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
