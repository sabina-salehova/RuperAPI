using Microsoft.EntityFrameworkCore;
using Ruper.DAL.Base;
using Ruper.DAL.DataContext;
using Ruper.DAL.Repositories.Contracts;
using System.Threading.Tasks;

namespace Ruper.DAL.Repositories
{
    public class EfCoreRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext _dbContext;

        public EfCoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async virtual Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await _dbContext.AddRangeAsync(entities.ToList());
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(params T[] entities)
        {
            await _dbContext.AddRangeAsync(entities.ToList());
            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.Set<T>().FindAsync(id);

            if (deletedEntity is null) throw new Exception();

            _dbContext.Set<T>().Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async virtual Task<IList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();           
        }

        public async virtual Task<IList<T>> GetAllIsNotDeletedAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(x=>!x.IsDeleted).ToListAsync();
        }

        public async virtual Task<T> GetAsync(int? id)
        {
            if (id is null) throw new Exception();

            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async virtual Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
