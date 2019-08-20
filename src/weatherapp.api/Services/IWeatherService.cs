using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<List<AveragedDayForecast>> GetForecast(string city, string zipCode);
    }
}
