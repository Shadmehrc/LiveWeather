using Application.DTO;
using Application.Interface.ServiceInterface;
using Domain.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Infrastructure.ExternalServices
{
    public class WeatherCrawller : IWeatherCrawller
    {
        private readonly OpenWeatherConfig _weatherConfig;
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherCrawller> _logger;

        public WeatherCrawller(HttpClient http, IOptions<OpenWeatherConfig> weatherConfig, ILogger<WeatherCrawller> logger)
        {
            _httpClient = http;
            _weatherConfig = weatherConfig.Value;
            _logger = logger;
        }

        public async Task<CityEnvironmentInfoDto> GetByCityName(string cityName)
        {
            try
            {

                string openWeatherApiKey = _weatherConfig.ApiKey;

                string weatherUrl = _weatherConfig.WeatherURL.Replace("{cityName}", Uri.EscapeDataString(cityName))
                                                             .Replace("{apiKey}", openWeatherApiKey);

                OpenWeatherResponse weather = await GetJson<OpenWeatherResponse>(weatherUrl);

                double latitude = weather.coord.lat;
                double longitude = weather.coord.lon;

                string airPollutionUrl = _weatherConfig.AirURL.Replace("{latitude}", latitude.ToString())
                                                              .Replace("{longitude}", longitude.ToString())
                                                              .Replace("{apiKey}", openWeatherApiKey);

                AirPollutionResponse airPollution = await GetJson<AirPollutionResponse>(airPollutionUrl);

                AirMain main = airPollution.list.FirstOrDefault()?.main;
                AirComponents comp = airPollution.list.FirstOrDefault()?.components;

                var result = new CityEnvironmentInfoDto
                {
                    City = cityName,
                    Temperature = weather.main.temp,
                    Humidity = weather.main.humidity,
                    WindSpeed = weather.wind.speed,
                    AQI = main?.aqi,
                    Pollutants = comp,
                    Latitude = latitude,
                    Longitude = longitude
                };

                _logger.LogInformation("Successfully fetched environment info for {City}", cityName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get environment info for city: {City}", cityName);
                throw new Exception(ex.Message);
            }
        }
        private async Task<T> GetJson<T>(string url, int retryCount = 0, CancellationToken ct = default)
        {
            _logger.LogInformation("GET {Url} (try {Try})", url, retryCount + 1);
            try
            {
                using var resp = await _httpClient.GetAsync(url, ct);
                var body = await resp.Content.ReadAsStringAsync(ct);

                if (!resp.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Request failed ({Status}) on {Url}. Try {Try}", (int)resp.StatusCode, url, retryCount + 1);
                    if (retryCount > 3) throw new Exception($"Max retry reached for {url}");
                    return await GetJson<T>(url, retryCount + 1, ct);
                }

                _logger.LogInformation("Success ({Status}) {Url}", (int)resp.StatusCode, url);
                return JsonSerializer.Deserialize<T>(body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception on {Url} (try {Try})", url, retryCount);
                throw new Exception(ex.Message);
            }
        }
    }
}

