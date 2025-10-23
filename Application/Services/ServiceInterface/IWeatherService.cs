
using Domain.Models.Models;
using LiveWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServiceInterface
{
    public interface IWeatherService
    {
        public Task<CityEnvironmentInfoDto> GetByCityName(string CityName);
        public Task<bool> CheckToken();
    
    }

}
