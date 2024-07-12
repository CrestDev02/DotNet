using System.Linq.Expressions;

namespace UserManagement.Database.Entity.Generic
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        //Add single entity
        Task<T> AddAsync(T t);

        //Add multiple entities
        Task<int> AddRangeAsync(IList<T> t);

        //Count total records
        Task<int> CountAsync();

        //Delete specific entity
        Task<int> DeleteAsync(T entity);

        //Delete all entities
        void DeleteAll(Expression<Func<T, bool>> match);

        //Find multiple entities
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match);

        //Find single entity
        Task<T?> FindAsync(Expression<Func<T, bool>> match);

        //Find entity by condition
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);

        //Get all entities
        Task<IEnumerable<T>> GetAllAsync();

        //Get entity by Id
        Task<T?> GetAsync(int id);

        //Create new entity
        Task<int> SaveAsync();

        //Update existing entity
        Task<T> UpdateAsync(T t, object key);

        //Delete all entities
        void DetachAllEntities();
    }
}
