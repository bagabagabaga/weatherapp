﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class AveragedDayForecastModel
    {
        public DateTime Date { get; set; }
        public double AveragedTemperature { get; set; }
        public double AveragedWind { get; set; }
        public double AveragedHumidity { get; set; }
    }
}
