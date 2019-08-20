using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Domain;

namespace WeatherApp.Services
{
    public interface ICitiesService
    {
        List<City> GetCities(string name, int citiesCount);
    }
}