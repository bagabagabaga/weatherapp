using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApp.Domain
{
    public class SixDayForecast
    {
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "list")]
        public List<Forecast> DailyForecastList { get; set; }

        [JsonProperty(PropertyName = "city")]
        public City City { get; set; }
    }
}
