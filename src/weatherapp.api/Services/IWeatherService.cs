using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Domain;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<WeatherForecastModel> GetForecast(string cityId, string zipCode);
        Task<CityForecast> CreateCityForecast(string cityName, DateTime date, double humidity, double temperature);
        Task<List<CityForecast>> GetForecastHistory();
    }
}
