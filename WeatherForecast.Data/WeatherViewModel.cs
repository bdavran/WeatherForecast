using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Data
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }

        // Google Maps için eklendi:
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
