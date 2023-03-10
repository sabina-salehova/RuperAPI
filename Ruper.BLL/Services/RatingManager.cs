using Microsoft.EntityFrameworkCore;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.DataContext;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories;

namespace Ruper.BLL.Services
{
    public class RatingManager : EfCoreRepository<Rating> , IRatingService
    {
        private readonly AppDbContext _dbContext;
        public RatingManager(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task AddAsync(Rating entity)
        {
            Product product = await _dbContext.Products.FindAsync(entity.ProductId);

            if (product is null) throw new Exception();

            ApplicationUser user = await _dbContext.Users.FindAsync(entity.UserId);

            if (user is null) throw new Exception();

            if (entity.IsDeleted == false && product.IsDeleted == true)
                throw new Exception();

            var sameProductRateSameUser = await _dbContext.Ratings
                                         .Where(x => x.ProductId == entity.ProductId && entity.UserId == x.UserId)
                                         .FirstOrDefaultAsync();

            if (sameProductRateSameUser is not null) throw new Exception();

            var productRates= await _dbContext.Ratings
                                    .Where(x => x.ProductId == entity.ProductId)
                                    .ToListAsync();

            double productRatesSum = entity.Rate;

            if (productRates.Count > 0)
            {                
                await _dbContext.Ratings.Where(x => x.ProductId == entity.ProductId)
                                        .ForEachAsync(y => productRatesSum += y.Rate);

                product.Rate = (double)(productRatesSum / (productRates.Count+1));
                _dbContext.Products.Update(product);
            }
            else if (productRates.Count == 0)
            {
                product.Rate = productRatesSum;
                _dbContext.Products.Update(product);
            }

            await base.AddAsync(entity);                        
        }
    }
}
