using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Domain
{
    public class FiveDayForecast
    {
        public int cod { get; set; }
        public List<Forecast> list { get; set; }
    }
}
