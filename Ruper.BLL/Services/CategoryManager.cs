using Microsoft.EntityFrameworkCore;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.DataContext;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.BLL.Services
{
    public class CategoryManager : EfCoreRepository<Category>, ICategoryService
    {
        private readonly AppDbContext _dbContext;
        public CategoryManager(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public override async Task DeleteAsync(Category entity)
        {
            var deletedEntity = await _dbContext.Categories
                .Where(x => x.Name == entity.Name.Trim())
                .FirstOrDefaultAsync();

            if (deletedEntity is null) throw new Exception();

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
