
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Models;
using LiveWeather;

namespace Infrastructure.DAL.RepositoryInterface
{
    public interface IWeatherRepository
    {
        public Task<CityEnvironmentInfoDto> GetByCityName(string cityName);
    }
}
