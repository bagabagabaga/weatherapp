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

        public CitiesService(WeatherAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<City>> GetCities(string name, int citiesCount)
        {
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

            return cities.ToList();
        }

    }
}
