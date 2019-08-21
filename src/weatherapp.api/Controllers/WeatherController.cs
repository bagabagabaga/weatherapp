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
        public async Task<ActionResult<List<AveragedDayForecastModel>>> Get(string cityId, string zipCode)
        {
            var forecast = await _weatherService.GetForecast(cityId, zipCode);

            if (forecast != null)
            {
                return forecast;
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost("forecast")]
        public async Task<ActionResult> Create(CityForecastInputModel model)
        {
            var cityForecast = await _weatherService.CreateCityForecast(model.CityName, model.Date, model.Humidity, model.Temperature);
            return Ok();
        }


        [HttpGet("history")]
        public async Task<ActionResult<List<CityForecast>>> GetHistory()
        {
            var forecastHistory = await _weatherService.GetForecastHistory();
            return Ok(forecastHistory);
        }
    }
}
