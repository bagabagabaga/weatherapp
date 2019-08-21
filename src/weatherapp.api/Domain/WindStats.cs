using Newtonsoft.Json;

namespace WeatherApp.Domain
{
    public class WindStats
    {
        [JsonProperty(PropertyName = "speed")]
        public double Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public double Degree { get; set; }
    }
}
