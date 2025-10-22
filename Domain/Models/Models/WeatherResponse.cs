using Domain.Models.Models;

namespace LiveWeather
{
    public class WeatherResponse
    {
        public Coord coord { get; set; } = default!;
        public Main main { get; set; } = default!;
        public Wind wind { get; set; } = default!;
    }

    public class Coord { public double lon { get; set; } public double lat { get; set; } }
    public class Main { public double temp { get; set; } public int humidity { get; set; } }
    public class Wind { public double speed { get; set; } }


    public sealed record CityEnvDto(
  string City,
  double Temperature,
  int Humidity,
  double WindSpeed,
  int? AQI,
  AirComponents Pollutants,
  double Latitude,
  double Longitude
);
}
