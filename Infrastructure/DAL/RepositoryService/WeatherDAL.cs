using Domain.Models.Models;
using Infrastructure.DAL.RepositoryInterface;
using Infrastructure.Persistence;
using LiveWeather;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Infrastructure.DAL.RepositoryService
{
    public class WeatherDAL : IWeatherDAL
    {

        private readonly HttpClient _httpClient;

        public WeatherDAL(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        private readonly string apiKey = "6f2bc3a65a8cbc5d6a0415aa96a5a946";
        public async Task<CityEnvDto> GetByCityName(string cityName)
        {
            try
            {
                var weatherUrl = $"https://api.openweathermap.org/data/2.5/weather?q={Uri.EscapeDataString(cityName)}&appid={apiKey}&units=metric";
                var weather = await http.GetFromJsonAsync<WeatherResponse>(weatherUrl);
                //if (weather is null) return null;

                var (lat, lon) = (weather.coord.lat, weather.coord.lon);

                var airUrl = $"https://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={apiKey}";
                var air = await _httpClient.GetFromJsonAsync<AirPollutionResponse>(airUrl);

                var main = air?.list?.FirstOrDefault()?.main;
                var comp = air?.list?.FirstOrDefault()?.components ?? new AirComponents();

                return new CityEnvDto(
                             City: cityName,
                             Temperature: weather.main.temp,
                             Humidity: weather.main.humidity,
                             WindSpeed: weather.wind.speed,
                             AQI: main?.aqi,
                             Pollutants: comp,
                             Latitude: lat,
                             Longitude: lon);
            }
            catch (Exception msg)
            {
                throw new Exception(msg.Message);

            }


        }


    }
}
