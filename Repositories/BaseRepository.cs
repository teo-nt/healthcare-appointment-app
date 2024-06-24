
using HealthcareAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly HealthCareDbContext _context;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(HealthCareDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public virtual async Task<T?> DeleteAsync(long id)
        {
            T? existing = await _dbSet.FindAsync(id);
            if (existing is null)
            {
                return null;
            }
            _dbSet.Remove(existing);
            return existing;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;
        }

        public void Detach(T entity)
        {
            _dbSet.Entry(entity).State = EntityState.Detached;
        }
    }
}
