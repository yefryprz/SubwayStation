using Microsoft.Extensions.DependencyInjection;
using SubwayStation.Domain.Repository.Contracts;

namespace SubwayStation.Domain.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _services;

        public RepositoryFactory() { }

        public RepositoryFactory(IServiceProvider services)
        {
            _services = services;
        }

        public IRepository<TEntity> GetDataRepository<TEntity>() where TEntity : class, new()
        {
            var instance = _services.GetService<IRepository<TEntity>>();
            return instance;
        }
    }
}
