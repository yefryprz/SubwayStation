using Microsoft.Extensions.DependencyInjection;
using SubwayStation.Domain.Repository;
using SubwayStation.Domain.Repository.Contracts;

namespace SubwayStation.Infrastructure.ConfigInjections
{
    public static class RepositoryInjection
    {
        public static void AddRepositoryInjections(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryFactory, RepositoryFactory>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
