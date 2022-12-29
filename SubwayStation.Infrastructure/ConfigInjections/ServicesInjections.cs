using Microsoft.Extensions.DependencyInjection;
using SubwayStation.Appication;
using SubwayStation.Application.Contracts;
using SubwayStation.Contracts;
using SubwayStation.Infrastructure.Services;

namespace SubwayStation.Infrastructure.ConfigInjections
{
    public static class ServicesInjections
    {
        public static void AddServicesInjections(this IServiceCollection services)
        {
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<ISubwayService, SubwayService>();
        }
    }
}
