using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Domain
{
    public class Forecast
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public float Temperature { get; set; }
        public float WindSpeed { get; set; }
        public float Humidity { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
    }
}
