using Application.DTO;
using Application.Interface.ServiceInterface;
using Application.Model;
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
        [HttpGet("city/{city}")]
        public async Task<ActionResult<ApiResponse<CityEnvironmentInfoDto>>> GetWeatherByName(string city, CancellationToken ct)
        {
            try
            {
                var result = await _weatherService.GetByCityName(city);
                return Ok(ApiResponse<CityEnvironmentInfoDto>.Ok(result));
            }
            catch (Exception ex)
            {
                return StatusCode(502, ApiResponse<CityEnvironmentInfoDto>.Fail("Error occured", ex.Message));
            }
        }
    }
}