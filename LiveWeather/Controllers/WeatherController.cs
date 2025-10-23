using Application.Services.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace LiveWeather.Controllers
{
    [ApiController]
    [Route("Main")]
    public class WeatherControllerController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherControllerController(IWeatherService _weatherService)
        {
            this._weatherService = _weatherService;
        }

        //private readonly ILogger<CrawlerController> _logger;
        //public CrawlerController(ILogger<CrawlerController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet("City/{city}")]
        public async Task<IActionResult> GetWeatherByName(string city)
        {
            var result = await _weatherService.GetByCityName(city);
            return Ok(result);
        }

    }

}