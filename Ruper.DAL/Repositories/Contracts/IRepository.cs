using Ruper.DAL.Base;

namespace Ruper.DAL.Repositories.Contracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetAllIsNotDeletedAsync();
        Task<T> GetAsync(int? id);
        Task UpdateAsync(T entity);
        Task CompletelyDeleteAsync(int? id);
        Task DeleteAsync(T entity);
        Task AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);
        Task AddAsync(params T[] entities);
    }
}
