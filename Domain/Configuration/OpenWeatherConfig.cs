using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration
{
    public class OpenWeatherConfig
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string WeatherURL { get; set; }
        public string AirURL { get; set; }
    }

}
