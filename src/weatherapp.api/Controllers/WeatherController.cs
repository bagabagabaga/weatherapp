using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IWeatherService _weatherService { get; }

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }


        [HttpGet("forecast")]
        public async Task<List<AveragedDayForecast>> GetAsync(string cityId, string zipCode)
        {
            var forecast = await _weatherService.GetForecast(cityId, zipCode);
            return forecast;
        }
    }
}
