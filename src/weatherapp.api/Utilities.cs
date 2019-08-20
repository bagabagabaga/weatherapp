using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Domain;

namespace WeatherApp
{
    public static class Utilities
    {
        public static List<City> GetGermanCities()
        {
            var pathToCities = GetAppConfig()["WeatherAppConfig:SeedFilePath"];
            var allCities = File.ReadAllText(pathToCities);
            if (allCities != null)
            {
                var citiesDeserialized = JsonConvert.DeserializeObject<List<City>>(allCities);
                return citiesDeserialized.Where(x => x.Country == "DE").ToList();
            }

            return null;
        }       


        private static IConfigurationRoot GetAppConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
