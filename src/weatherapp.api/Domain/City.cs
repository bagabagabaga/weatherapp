using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Domain
{
    public class City
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}
