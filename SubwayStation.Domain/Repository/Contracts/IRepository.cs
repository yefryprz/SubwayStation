using System.Linq.Expressions;

namespace SubwayStation.Domain.Repository.Contracts
{
    public interface IRepository { }

    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entityList);

        Task RemoveAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);

        Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null);

        Task<IEnumerable<T>> GetAllAsync(string sortExpression = null);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string sortExpression = null);

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);

        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null);
    }
}
