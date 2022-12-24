using SubwayStation.Domain.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubwayStation.Domain.Repository
{
    public class Repository<TEntity> : RepositoryBase<TEntity, SubwayStationContext> where TEntity : class, new()
    {
        public Repository(SubwayStationContext context) : base(context) { }
    }
}
