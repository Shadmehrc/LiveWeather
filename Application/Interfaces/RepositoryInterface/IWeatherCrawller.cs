
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;


namespace Application.Interface.ServiceInterface
{
    public interface IWeatherCrawller
    {
        public Task<CityEnvironmentInfoDto> GetByCityName(string cityName);
    }
}
