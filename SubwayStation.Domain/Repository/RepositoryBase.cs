using Microsoft.EntityFrameworkCore;
using SubwayStation.Domain.Repository.Contracts;
using System.Linq.Expressions;

namespace SubwayStation.Domain.Repository
{
    public class RepositoryBase<T, C> : IRepository<T>, IDisposable where T : class where C : DbContext
    {
        private readonly C _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(C context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entityList)
        {
            await _context.Set<T>().AddRangeAsync(entityList);
            await _context.SaveChangesAsync();
            return entityList;
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry<T>(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            var query = await Task.FromResult(_dbSet.AsNoTracking().Where(filter));
            return query?.FirstOrDefault();
        }

        public async Task<TResult> GetAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null)
        {
            var query = filter == null ? _dbSet.AsNoTracking() : _dbSet.Where(filter).AsNoTracking();
            var notSortedResults = transform(query);
            return await notSortedResults.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string sortExpression = null)
        {
            var query = _dbSet.AsNoTracking();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string sortExpression = null)
        {
            var query = _dbSet.AsNoTracking().Where(filter);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _dbSet.AsNoTracking() : _dbSet.AsNoTracking().Where(filter);
            var notSortedResults = transform(query);
            return await notSortedResults.ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform, Expression<Func<T, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _dbSet.AsNoTracking() : _dbSet.AsNoTracking().Where(filter);
            var notSortedResults = transform(query);
            return await notSortedResults.ToListAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
