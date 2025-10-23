using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class CityEnvironmentInfoDto
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public int? AQI { get; set; }
        public AirComponents? Pollutants { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public CityEnvironmentInfoDto() { }
    }
}
