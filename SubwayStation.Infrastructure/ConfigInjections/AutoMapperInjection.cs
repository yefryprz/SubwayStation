using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SubwayStation.Infrastructure.MapperProfiles;

namespace SubwayStation.Infrastructure.ConfigInjections
{
    public static class AutoMapperInjection
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new AccountProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
