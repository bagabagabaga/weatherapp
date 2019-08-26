using Microsoft.Extensions.Logging;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data;
using WeatherApp.Domain;

namespace WeatherApp.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly WeatherAppDbContext _dbContext;
        private readonly ILogger<CitiesService> _logger;

        public CitiesService(WeatherAppDbContext dbContext, ILogger<CitiesService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<City> GetCities(string name, int citiesCount)
        {
            _logger.LogInformation($"Getting cities name={name} citiesCount={citiesCount}.");

            var cities = _dbContext.Cities
                .DistinctBy(x=>x.Name)
                .Where(city => city.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Name)
                .Take(citiesCount);


            if (cities.Count() == 0)
            {
                cities = _dbContext.Cities
                    .DistinctBy(x => x.Name)
                    .Where(city => city.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.Name)
                    .Take(citiesCount);
            }

            _logger.LogInformation($"Returned {cities.Count()} cities.");

            return cities.ToList();
        }

    }
}
