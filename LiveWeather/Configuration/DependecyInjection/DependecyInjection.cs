using Application.Services.Service;
using Application.Services.ServiceInterface;
using Infrastructure.DAL.RepositoryInterface;
using Infrastructure.DAL.RepositoryService;

namespace LiveWeather.Configuration.DependecyInjection
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IStateStoreRepository, SqlStateStoreRepository>();
            return services;
        }
    }
}
