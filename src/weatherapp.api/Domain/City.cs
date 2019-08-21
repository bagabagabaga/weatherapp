using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Domain
{
    public class City
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

    }
}
