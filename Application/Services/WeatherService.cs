using Application.Interface.ServiceInterface;
using Application.DTO;


namespace Application.Services
{
    public class WeatherService : IWeatherService
    {

        private readonly IWeatherCrawller _context;

        public WeatherService(IWeatherCrawller context)
        {
            _context = context;
        }

        public async Task<CityEnvironmentInfoDto> GetByCityName(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                throw new Exception("cityName is required.");

            return await _context.GetByCityName(cityName);
        }
    }
}
