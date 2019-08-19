using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Domain;

namespace WeatherApp.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var germanCities = Utilities.GetGermanCities();

            modelBuilder.Entity<City>().HasData(germanCities);
        }
    }
}
