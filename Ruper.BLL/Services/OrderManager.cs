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
    public class OrderManager : EfCoreRepository<Order> , IOrderService
    {
        private readonly AppDbContext _dbContext;
        public OrderManager(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task AddAsync(Order entity)
        {
            ProductColor productColor = await _dbContext.ProductColors.FindAsync(entity.ProductColorId);

            if (productColor is null) throw new Exception();

            ApplicationUser user = await _dbContext.Users.FindAsync(entity.UserId);

            if (user is null) throw new Exception();

            if (entity.IsDeleted == false && productColor.IsDeleted == true)
                throw new Exception();

            if (entity.Quantity > productColor.Quantity || productColor.Quantity <= 0)
                throw new Exception();

            Product product = await _dbContext.Products.FindAsync(productColor.ProductId);

            entity.Price = product.Price;
            entity.DisCount = product.DisCount;
            entity.TotalPrice = product.Price*entity.Quantity;

            productColor.Quantity = productColor.Quantity - entity.Quantity;
            _dbContext.ProductColors.Update(productColor);            

            await base.AddAsync(entity);
        }
    }
}
