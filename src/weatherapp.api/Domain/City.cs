using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
