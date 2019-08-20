using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.Domain;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService _citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            _citiesService = citiesService;
        }

      
        // GET: api/Cities?name=nameofCity
        [HttpGet]
        public List<City> GetCities(string name, int citiesCount = 5)
        {
            var cities =  _citiesService.GetCities(name, citiesCount);
            return cities;
        }
    }
}
