using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Domain
{
    public class Forecast
    {
        public int dt { get; set; }
        public WeatherConditions  main { get; set; }
        public List<WeatherDescription> weather { get; set; }
        public WindStats wind { get; set; }
        public string dt_txt { get; set; }
    }
}
