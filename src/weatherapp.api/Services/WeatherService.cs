using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<WeatherService> _logger;

        private string WeatherCache { get { return "_WeatherCache-CityId={0}-zipCode={1}"; } }

        public WeatherService(ICitiesService citiesService, HttpClient httpClient, IOptions<WeatherAppConfig> config, IMemoryCache memoryCache, WeatherAppDbContext dbContext, ILogger<WeatherService> logger)
        {
            _citiesService = citiesService;

            httpClient.BaseAddress = new Uri("http://api.openweathermap.org/");
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
            _cache = memoryCache;
            _dbContext = dbContext;
        }


        public async Task<CityForecast> CreateCityForecast(string cityName, DateTime date, double humidity, double temperature)
        {
            _logger.LogDebug($"Added a city={cityName} on date={date}, humidity={humidity}, temperature={temperature}");

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
            _logger.LogDebug("Getting history of forecasts.");

            var history = await _dbContext.CityForecasts.ToListAsync();
            return history;
        }


        public async Task<WeatherForecastModel> GetForecast(string cityId, string zipCode)
        {
            _logger.LogDebug($"Getting forecasts for city ID = {cityId} and zipcode = {zipCode}");

            //OpenWeatherMap: We recommend making calls to the API no more than one time every 10 minutes for one location
            //let's cache the response for 10 minutes
            WeatherForecastModel weatherForecast;
            if (!_cache.TryGetValue(String.Format(WeatherCache, cityId, zipCode), out weatherForecast))
            {
                _logger.LogDebug($"Missing cache for city ID = {cityId} and zipcode = {zipCode}");

                WeatherForecastModel sixDayForecast = await GetSixDayForecast(cityId, zipCode);

                //sometimes (after 23.00) the openweathermap api returns five next days but not today
                //in that case we fill the today's value from another endpoint
                if (sixDayForecast?.Forecasts.Count() < 6) { 
                    AveragedDayForecastModel currentForecast = await GetCurrentForecast(cityId, zipCode);
                    sixDayForecast.Forecasts.Insert(0, currentForecast);
                }

                weatherForecast = sixDayForecast;
                _cache.Set(string.Format(WeatherCache, cityId, zipCode), sixDayForecast, TimeSpan.FromMinutes(10));
            }

            return weatherForecast;
        }


        #region PrivateMethods

        private async Task<AveragedDayForecastModel> GetCurrentForecast(string cityId, string zipCode)
        {
            string endpoint = GetEndpoint(cityId, zipCode, "weather");
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                var currentWeather = JsonConvert.DeserializeObject<Forecast>(text);

                return new AveragedDayForecastModel()
                {
                    AveragedHumidity = currentWeather.WeatherConditions.Humidity,
                    AveragedTemperature = currentWeather.WeatherConditions.Temperature,
                    AveragedWind = currentWeather.WindStats.Speed,
                    Date = DateTime.Now.Date
                };
            }

            return null;
        }

        private async Task<WeatherForecastModel> GetSixDayForecast(string cityId, string zipCode)
        {
            string endpoint = GetEndpoint(cityId, zipCode, "forecast");
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var text = await response.Content.ReadAsStringAsync();
                var sixDayForecast = JsonConvert.DeserializeObject<SixDayForecast>(text);

                var averagedFiveDayForecast = GetAveragedSixDayForecast(sixDayForecast);

                var weatherForecast = new WeatherForecastModel()
                {
                    City = sixDayForecast.City,
                    Forecasts = averagedFiveDayForecast
                };

                return weatherForecast;
            }
            else
            {
                return null;
            }


        }


        private List<AveragedDayForecastModel> GetAveragedSixDayForecast(SixDayForecast fiveDayForecast)
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


        private string GetEndpoint(string cityId, string zipCode, string apiRoute)
        {
            var apiKey = _config.Value.OpenWeatherApiKey;
            var endpoint = $"/data/2.5/{apiRoute}?APPID={apiKey}";

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
