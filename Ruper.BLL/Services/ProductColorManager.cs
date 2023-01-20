using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Ruper.BLL.Data;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.Base;
using Ruper.DAL.DataContext;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Ruper.BLL.Services
{
    public class ProductColorManager : EfCoreRepository<ProductColor> , IProductColorService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ProductColorManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }


        public override async Task AddAsync(ProductColor entity)
        {
            Color color = await _dbContext.Colors.FindAsync(entity.ColorId);

            if(color is null) throw new Exception();            

            Product product = await _dbContext.Products.FindAsync(entity.ProductId);

            if (product is null) throw new Exception();

            if (entity.IsDeleted == false)
            {
                if (product.IsDeleted == true || color.IsDeleted == true)
                    throw new Exception();
            }

            var sameColorProduct = await _dbContext.ProductColors
                                         .Where(x=>x.ColorId== entity.ColorId && entity.ProductId==x.ProductId)
                                         .FirstOrDefaultAsync();

            if(sameColorProduct is not null) throw new Exception();

            await base.AddAsync(entity);
        }

        //public override async Task CompletelyDeleteAsync(int? id)
        //{
        //    if (id is null) throw new Exception();

        //    var deletedEntity = await _dbContext.SubCategories.FindAsync(id);

        //    if (deletedEntity is null) throw new Exception();

        //    var product = await _dbContext.Products.Where(x => x.SubCategoryId == id)
        //                                                     .FirstOrDefaultAsync();

        //    if (product is not null) throw new Exception();

        //    var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "SubCategory", deletedEntity.ImageName);

        //    if (File.Exists(path))
        //        File.Delete(path);

        //    _dbContext.Remove(deletedEntity);
        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task UpdateById(int? id, SubCategoryUpdateDto subCategoryUpdateDto)
        {
            if (id is null) throw new Exception();

            var existSubCategory = await _dbContext.SubCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existSubCategory is null) throw new Exception();

            if (subCategoryUpdateDto.Id != id) throw new Exception();

            if (subCategoryUpdateDto.Image is not null)
            {
                var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "SubCategory");
                var existImageName = Path.Combine(forderPath, existSubCategory.ImageName);

                if (File.Exists(existImageName))
                    File.Delete(existImageName);

                subCategoryUpdateDto.ImageName = await subCategoryUpdateDto.Image.GenerateFile(forderPath);

            }
            else subCategoryUpdateDto.ImageName = existSubCategory.ImageName;

            if (subCategoryUpdateDto.Name is null) subCategoryUpdateDto.Name = existSubCategory.Name;

            if (subCategoryUpdateDto.IsDeleted is null)
            {
                subCategoryUpdateDto.IsDeleted = existSubCategory.IsDeleted;
            }

            if (subCategoryUpdateDto.CategoryId is null)
            {
                subCategoryUpdateDto.CategoryId = existSubCategory.CategoryId;
            }

            Category category = await _dbContext.Categories.FindAsync(subCategoryUpdateDto.CategoryId);

            if (category is null) throw new Exception();

            if (subCategoryUpdateDto.IsDeleted is null)
            {
                subCategoryUpdateDto.IsDeleted = existSubCategory.IsDeleted;
            }
            else
            {
                var product = await _dbContext.Products
                    .Where(x => x.SubCategoryId == id && x.IsDeleted == false)
                    .FirstOrDefaultAsync();

                if (subCategoryUpdateDto.IsDeleted == true && product != null) throw new Exception();
            }

            if (subCategoryUpdateDto.IsDeleted == false && category.IsDeleted == true) throw new Exception();

            var subCategory = _mapper.Map<SubCategory>(subCategoryUpdateDto);

            _dbContext.SubCategories.Update(subCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
