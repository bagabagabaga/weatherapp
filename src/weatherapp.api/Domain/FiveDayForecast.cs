using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApp.Domain
{
    public class FiveDayForecast
    {
        [JsonProperty(PropertyName = "cod")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "list")]
        public List<Forecast> DailyForecastList { get; set; }
    }
}
