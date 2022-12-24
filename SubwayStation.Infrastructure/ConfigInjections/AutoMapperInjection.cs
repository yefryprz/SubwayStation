using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SubwayStation.Infrastructure.ConfigInjections
{
    public static class AutoMapperInjection
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                //config.AddProfile(new RatingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
