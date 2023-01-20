using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.DataContext;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories;

namespace Ruper.BLL.Services
{
    public class ProductManager : EfCoreRepository<Product> , IProductService
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ProductManager(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        
        public override async Task AddAsync(Product entity)
        {
            SubCategory subCategory = await _dbContext.SubCategories.FindAsync(entity.SubCategoryId);

            if(subCategory is null) throw new Exception();

            Brand brand = await _dbContext.Brands.FindAsync(entity.BrandId);

            if (brand is null) throw new Exception();

            if (entity.IsDeleted == false)
            {
                if(subCategory.IsDeleted == true || brand.IsDeleted==true)
                throw new Exception();
            }

            await base.AddAsync(entity);
        }

        //productColor-dan sonra duzeliw edilmeli
        public override async Task CompletelyDeleteAsync(int? id)
        {
            if (id is null) throw new Exception();

            var deletedEntity = await _dbContext.Products.FindAsync(id);

            if (deletedEntity is null) throw new Exception();

            //var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "SubCategory", deletedEntity.ImageName);

            //if (File.Exists(path))
            //    File.Delete(path);

            _dbContext.Remove(deletedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateById(int? id, ProductUpdateDto productUpdateDto)
        {
            if (id is null) throw new Exception();

            var existProduct = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (existProduct is null) throw new Exception();

            if (productUpdateDto.Id != id) throw new Exception();

            //if (subCategoryUpdateDto.Image is not null)
            //{
            //    var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "SubCategory");
            //    var existImageName = Path.Combine(forderPath, existSubCategory.ImageName);

            //    if (File.Exists(existImageName))
            //        File.Delete(existImageName);

            //    subCategoryUpdateDto.ImageName = await subCategoryUpdateDto.Image.GenerateFile(forderPath);

            //}
            //else subCategoryUpdateDto.ImageName = existSubCategory.ImageName;

            if (productUpdateDto.Name is null) productUpdateDto.Name = existProduct.Name;

            if (productUpdateDto.Description is null) productUpdateDto.Description = existProduct.Description;

            if (productUpdateDto.Price is null) productUpdateDto.Price = existProduct.Price;

            if (productUpdateDto.DisCount is null) productUpdateDto.DisCount = existProduct.DisCount;

            if (productUpdateDto.Rate is null) productUpdateDto.Rate = existProduct.Rate;

            if (productUpdateDto.IsDeleted is null)
            {
                productUpdateDto.IsDeleted = existProduct.IsDeleted;
            }            

            if (productUpdateDto.SubCategoryId is null)
            {
                productUpdateDto.SubCategoryId = existProduct.SubCategoryId;
            }

            SubCategory subCategory = await _dbContext.SubCategories.FindAsync(productUpdateDto.SubCategoryId);

            if (subCategory is null) throw new Exception();

            if (productUpdateDto.BrandId is null)
            {
                productUpdateDto.BrandId = existProduct.BrandId;
            }

            Brand brand = await _dbContext.Brands.FindAsync(productUpdateDto.BrandId);

            if (brand is null) throw new Exception();

            if (productUpdateDto.IsDeleted == false)
            {
                if(subCategory.IsDeleted == true || brand.IsDeleted == true)
                throw new Exception();
            }

            var product = _mapper.Map<Product>(productUpdateDto);

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<GeneralProductColorDto>> GetGeneralProducts()
        {
            var products = await _dbContext.Products.ToListAsync();

            if (products.Count == 0)
                throw new Exception();

            var generalProductsDtos = _mapper.Map<List<GeneralProductDto>>(products);

            //subCategoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/subcategory/" + x.ImageName);

            var brands = await _dbContext.Brands.ToListAsync();
            generalProductsDtos.ForEach(x => x.BrandName = brands.Where(y => y.Id == x.BrandId).FirstOrDefault().Name);

            var subCategories = await _dbContext.SubCategories.ToListAsync();
            generalProductsDtos.ForEach(x => x.SubCategoryName = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().Name);
            generalProductsDtos.ForEach(x => x.CategoryId = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().CategoryId);

            var categories = await _dbContext.Categories.ToListAsync();
            generalProductsDtos.ForEach(x => x.CategoryId = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().CategoryId);
            generalProductsDtos.ForEach(x => x.CategoryName = categories.Where(y => y.Id == x.CategoryId).FirstOrDefault().Name);

            var productColors = await _dbContext.ProductColors.ToListAsync();

            generalProductsDtos.ForEach(x => x.GeneralProductColors = _mapper.Map<List<GeneralProductColorDto>>(productColors.Where(y => y.ProductId == x.Id).ToList()));

            var colors = await _dbContext.Colors.ToListAsync();

            generalProductsDtos.ForEach(x => x.GeneralProductColors.ForEach(y => y.ColorName = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorName));
            generalProductsDtos.ForEach(x => x.GeneralProductColors.ForEach(y => y.ColorCode = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorCode));

            return (IList<GeneralProductColorDto>)generalProductsDtos;
        }
    }
}
