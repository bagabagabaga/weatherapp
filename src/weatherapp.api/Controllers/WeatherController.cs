using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Domain;
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
        public async Task<ActionResult<WeatherForecastModel>> Get(string cityId, string zipCode)
        {
            var forecast = await _weatherService.GetForecast(cityId, zipCode);

            if (forecast != null)
            {
                return Ok(forecast);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("history")]
        public async Task<ActionResult> CreateHistory(CityForecastInputModel model)
        {
            var cityForecast = await _weatherService.CreateCityForecast(model.CityName, model.Date, model.Humidity, model.Temperature);
            return Ok(model);
        }


        [HttpGet("history")]
        public async Task<ActionResult<List<CityForecast>>> GetHistory()
        {

            var forecastHistory = await _weatherService.GetForecastHistory();
            return Ok(forecastHistory);
        }
    }
}
