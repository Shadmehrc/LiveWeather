
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveWeather;

namespace Infrastructure.DAL.RepositoryInterface
{
    public interface IWeatherDAL
    {
        public Task<CityEnvDto> GetByCityName(string cityName);
    }
}
