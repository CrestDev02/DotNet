using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagement.Database.Entity.DataAccess.Context;

namespace UserManagement.Database.Entity.Generic
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext _context;

        private bool disposed = false;
        public GenericRepository(ApplicationContext context) => _context = context;
        public void DetachAllEntities()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries().ToList()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        //Get all entities
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        //Get all async entities
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        //Get entity by Id
        public virtual async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        //Create new entity
        public virtual async Task<T> AddAsync(T t)
        {
            await _context.Set<T>().AddAsync(t);
            await SaveAsync();
            return t;
        }

        //Create multiple entities
        public virtual async Task<int> AddRangeAsync(IList<T> t)
        {
            await _context.Set<T>().AddRangeAsync(t);
            return await SaveAsync();
        }

        //Find single entity
        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(match);
        }

        //Find entity by condition
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().AsNoTracking().Where(match).ToListAsync();
        }

        //Delete specific entity
        public virtual async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await SaveAsync();
        }

        //Delete all entities
        public virtual async void DeleteAll(Expression<Func<T, bool>> match)
        {
            IEnumerable<T> entities = _context.Set<T>().Where(match).AsNoTracking();

            if (!entities.Any())
                return;

            _context.RemoveRange(entities);
            await SaveAsync();
        }

        //Update existing entity
        public virtual async Task<T> UpdateAsync(T t, object? key)
        {
            if (t == null)
                return default!;

            T? exist = await _context.Set<T>().FindAsync(key);

            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await SaveAsync();
            }
            return exist ?? t;
        }

        //Count total records
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().AsNoTracking().CountAsync();
        }

        //Create new entity
        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        //Find entity by condition
        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        //Dispose unsed objects
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        //Dispose unsed objects
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
