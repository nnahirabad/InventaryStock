using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace InventarioComercio.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : DomainBaseModel
    {
        protected readonly InventarioComercioDbContext _context; 
        public RepositoryBase(InventarioComercioDbContext context)
        {
            _context = context;             
        }
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity; 
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity; 
            
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync(); 
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {

            return await _context.Set<T>().FindAsync(id);

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeString, bool disabledTracking)
        {

            IQueryable<T> query = _context.Set<T>();
            if (disabledTracking) query = query.AsNoTracking();

            if (string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, List<Expression<Func<T, object>>> includes, bool disabledTracking)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disabledTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
