using Newtonsoft.Json;

namespace WeatherApp.Domain
{
    public class WeatherDescription
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }
    }
}
