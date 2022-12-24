using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayStation.Domain.Repository.Contracts
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetDataRepository<TEntity>() where TEntity : class, new();
    }
}
