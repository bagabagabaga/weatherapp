using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Domain;
using WeatherApp.Services;

namespace WeatherApp.Test
{
    public class FakeCitiesService : ICitiesService
    {
        public List<City> GetCities(string name, int citiesCount)
        {
            return name == "existing" ? new List<City>() { new City {ID=0, Country="DE", Name="Existing" } } : new List<City>();
        }
    }
}
