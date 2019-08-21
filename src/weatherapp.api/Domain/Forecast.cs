using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApp.Domain
{
    public class Forecast
    {
        [JsonProperty(PropertyName = "main")]
        public WeatherConditions WeatherConditions { get; set; }

        [JsonProperty(PropertyName = "weather")]
        public List<WeatherDescription> WeatherDescription { get; set; }

        [JsonProperty(PropertyName = "wind")]
        public WindStats WindStats { get; set; }

        [JsonProperty(PropertyName = "dt_txt")]
        public string DateTimeText { get; set; }
    }
}
