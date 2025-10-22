using Application.Services.ServiceInterface;
using Infrastructure.DAL.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using LiveWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;


namespace Application.Services.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly IConfiguration _configuration;
        private readonly IWeatherDAL _context;

        public WeatherService(IConfiguration configuration, IWeatherDAL context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<CityEnvDto> GetByCityName(string cityName)
        {
            CheckToken();
            return await _context.GetByCityName(cityName);
        }

        public Task<bool> CheckToken()
        {


            throw new NotImplementedException();
        }

    }
}
