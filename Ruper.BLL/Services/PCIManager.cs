﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.DataContext;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories;

namespace Ruper.BLL.Services
{
    public class PCIManager : EfCoreRepository<ProductColorImage> , IPCIService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public PCIManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public override async Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.ProductColorImages.FindAsync(id);

            if (deletedEntity is null) throw new Exception();

            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Product", deletedEntity.ImageName);

            if (File.Exists(path))
                File.Delete(path);

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
