using Application.Interface.ServiceInterface;
using Application.Services;
using Infrastructure.ExternalServices;
using Infrastructure.RepositoryService;

namespace LiveWeather.Configuration.DependecyInjection
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddHttpClient<IWeatherCrawller, WeatherCrawller>();
            services.AddScoped<IStateStoreRepository, SqlStateStoreRepository>();
            return services;
        }
    }
}
