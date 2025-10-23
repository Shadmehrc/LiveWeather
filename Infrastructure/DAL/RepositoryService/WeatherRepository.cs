using Domain.Models.Models;
using Infrastructure.DAL.RepositoryInterface;
using LiveWeather;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Domain.Models.Models;

namespace Infrastructure.DAL.RepositoryService
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IConfiguration _configuration;

        public WeatherRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //if (weather is null) return null;

        public async Task<CityEnvironmentInfoDto> GetByCityName(string cityName)
        {
            try
            {
                HttpClient _httpClient = new HttpClient();

                string openWeatherApiKey = _configuration["OpenWeather:ApiKey"];

                string weatherUrl = _configuration["OpenWeather:WeatherURL"].Replace("{cityName}", Uri.EscapeDataString(cityName))
                                                                            .Replace("{apiKey}", openWeatherApiKey);
                OpenWeatherResponse weather = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(weatherUrl);

                double latitude = weather.coord.lat;
                double longitude = weather.coord.lon;

                string airPollutionUrl = _configuration["OpenWeather:AirURL"].Replace("{latitude}", latitude.ToString())
                                                                             .Replace("{longitude}", longitude.ToString())
                                                                             .Replace("{apiKey}", openWeatherApiKey);

                AirPollutionResponse airPollution = await _httpClient.GetFromJsonAsync<AirPollutionResponse>(airPollutionUrl);

                AirMain main = airPollution.list.FirstOrDefault().main;
                AirComponents comp = airPollution.list.FirstOrDefault().components;

                var result = new CityEnvironmentInfoDto
                {
                    City = cityName,
                    Temperature = weather.main.temp,
                    Humidity = weather.main.humidity,
                    WindSpeed = weather.wind.speed,
                    AQI = main.aqi,
                    Pollutants = comp,
                    Latitude = latitude,
                    Longitude = longitude
                };
                return result;

            }
            catch (Exception msg)
            {

                if (msg.Message.Contains("Response status code does not indicate success: 404 (Not Found)."))
                {
                    throw new Exception("City is not valid");
                }
                throw new Exception(msg.Message);

            }
        }
    }
}
