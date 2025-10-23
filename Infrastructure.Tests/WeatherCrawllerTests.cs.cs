using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Domain.Configuration;
using Infrastructure.ExternalServices;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Application.Interface.ServiceInterface;

public class WeatherCrawllerTests
{
    [Fact]
    public async Task GetByCityName_Returns_ValidData()
    {
        // Arrange
        var handler = new StubHandler(new Dictionary<string, (HttpStatusCode, string)>
        {
            {
                "weather", (HttpStatusCode.OK,
                "{\"coord\":{\"lat\":35.7,\"lon\":51.4},\"main\":{\"temp\":25,\"humidity\":50},\"wind\":{\"speed\":2}}")
            },
            {
                "air_pollution", (HttpStatusCode.OK,
                "{\"list\":[{\"main\":{\"aqi\":3},\"components\":{\"co\":1.1}}]}")
            }
        });

        var http = new HttpClient(handler);
        var config = Options.Create(new OpenWeatherConfig
        {
            ApiKey = "fake-key",
            WeatherURL = "https://test.com/weather?q={cityName}&appid={apiKey}",
            AirURL = "https://test.com/air_pollution?lat={latitude}&lon={longitude}&appid={apiKey}"
        });

        var stateRepo = new DummyStateRepo();
        var service = new WeatherCrawller(http, config, new NullLogger<WeatherCrawller>(), stateRepo);

        // Act
        var result = await service.GetByCityName("Tehran");

        // Assert
        result.City.Should().Be("Tehran");
        result.Temperature.Should().Be(25);
        result.AQI.Should().Be(3);
    }

    private sealed class DummyStateRepo : IStateStoreRepository
    {
        public bool OpenWeatherApiEnsureUsage() => true;
    }

    private sealed class StubHandler : HttpMessageHandler
    {
        private readonly Dictionary<string, (HttpStatusCode code, string body)> _responses;
        public StubHandler(Dictionary<string, (HttpStatusCode, string)> responses) => _responses = responses;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            foreach (var kv in _responses)
                if (request.RequestUri!.ToString().Contains(kv.Key))
                    return Task.FromResult(new HttpResponseMessage(kv.Value.code)
                    { Content = new StringContent(kv.Value.body) });
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
        }
    }
}