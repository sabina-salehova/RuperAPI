using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public RatingManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
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

            await base.AddAsync(entity);
        }

        //public override async Task CompletelyDeleteAsync(int? id)
        //{
        //    if (id is null) throw new Exception();

        //    var deletedEntity = await _dbContext.ProductColors.FindAsync(id);

        //    if (deletedEntity is null) throw new Exception();

        //    var images = await _dbContext.ProductColorImages.Where(x => x.ProductColorId == id).ToListAsync();

        //    foreach (var image in images) 
        //    {
        //        var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Product", image.ImageName);

        //        if (File.Exists(path))
        //            File.Delete(path);
        //    }

        //    _dbContext.Remove(deletedEntity);
        //    await _dbContext.SaveChangesAsync();
        //}

    }
}
