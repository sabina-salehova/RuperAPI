using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ruper.BLL.Data;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SubCategory> _subCategoryRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<ProductColor> _productColorRepository;
        private readonly IRepository<Color> _ColorRepository;
        private readonly IRepository<ProductColorImage> _productColorImageRepository;

        public ProductsController(IMapper mapper, IRepository<SubCategory> subCategoryRepository, IWebHostEnvironment webHostEnvironment, IRepository<Category> categoryRepository, IRepository<Brand> brandRepository, IRepository<Product> productRepository, IProductService productService, IRepository<ProductColor> productColorRepository, IRepository<Color> colorRepository, IRepository<ProductColorImage> productColorImageRepository)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _productService = productService;
            _productColorRepository = productColorRepository;
            _ColorRepository = colorRepository;
            _productColorImageRepository = productColorImageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneralProducts()
        {
            var products = await _productRepository.GetAllIsNotDeletedAsync();

            if (products.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");

            var generalProductsDtos = _mapper.Map<List<GeneralProductDto>>(products);

            var brands = await _brandRepository.GetAllAsync();
            generalProductsDtos.ForEach(x => x.BrandName = brands.Where(y => y.Id == x.BrandId).FirstOrDefault().Name);

            var subCategories = await _subCategoryRepository.GetAllAsync();
            generalProductsDtos.ForEach(x => x.SubCategoryName = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().Name);
            generalProductsDtos.ForEach(x => x.CategoryId = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().CategoryId);

            var categories = await _categoryRepository.GetAllAsync();
            generalProductsDtos.ForEach(x => x.CategoryId = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().CategoryId);
            generalProductsDtos.ForEach(x => x.CategoryName = categories.Where(y => y.Id == x.CategoryId).FirstOrDefault().Name);

            var productColors = await _productColorRepository.GetAllIsNotDeletedAsync();

            generalProductsDtos.ForEach(x => x.GeneralProductColors = _mapper.Map<List<GeneralProductColorDto>>(productColors.Where(y => y.ProductId == x.Id && !x.IsDeleted).ToList()));

            var productColorImages = await _productColorImageRepository.GetAllIsNotDeletedAsync();

            generalProductsDtos.ForEach(x => x.GeneralProductColors
                               .ForEach(y => y.GeneralProductColorImages = _mapper.Map<List<GeneralPCIDto>>(productColorImages.Where(z => z.ProductColorId == y.Id).ToList())));

            string imagePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/";

            generalProductsDtos.ForEach(x => x.GeneralProductColors
                               .ForEach(y => y.GeneralProductColorImages.ForEach(z=>z.ImageName=imagePath+z.ImageName)));

            var colors = await _ColorRepository.GetAllAsync();

            generalProductsDtos.ForEach(x => x.GeneralProductColors.ForEach(y => y.ColorName = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorName));
            generalProductsDtos.ForEach(x => x.GeneralProductColors.ForEach(y => y.ColorCode = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorCode));

            return Ok(generalProductsDtos);
        }

        [HttpGet("withoutColorsAndImages")]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAllAsync();

            if (products.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");

            var productsDtos = _mapper.Map<List<ProductDto>>(products);

            //subCategoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/subcategory/" + x.ImageName);

            var brands = await _brandRepository.GetAllAsync();
            productsDtos.ForEach(x => x.BrandName = brands.Where(y => y.Id == x.BrandId).FirstOrDefault().Name);

            var subCategories = await _subCategoryRepository.GetAllAsync();            
            productsDtos.ForEach(x => x.SubCategoryName = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().Name);
            productsDtos.ForEach(x => x.CategoryId = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().CategoryId);

            var categories = await _categoryRepository.GetAllAsync();
            productsDtos.ForEach(x => x.CategoryId = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().CategoryId);
            productsDtos.ForEach(x => x.CategoryName = categories.Where(y => y.Id == x.CategoryId).FirstOrDefault().Name);

            return Ok(productsDtos);
        }

        [HttpGet("isNotDeletedwithoutColorsAndImages")]
        public async Task<IActionResult> GetIsActive()
        {
            var products = await _productRepository.GetAllIsNotDeletedAsync();

            if (products.Count == 0)
                return NotFound("Hele hec bir delete olmayan product yaradilmayib");

            var productsDtos = _mapper.Map<List<ProductDto>>(products);

            //subCategoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/subcategory/" + x.ImageName);

            var brands = await _brandRepository.GetAllAsync();
            productsDtos.ForEach(x => x.BrandName = brands.Where(y => y.Id == x.BrandId).FirstOrDefault().Name);

            var subCategories = await _subCategoryRepository.GetAllAsync();
            productsDtos.ForEach(x => x.SubCategoryName = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().Name);

            var categories = await _categoryRepository.GetAllAsync();
            productsDtos.ForEach(x => x.CategoryId = subCategories.Where(y => y.Id == x.SubCategoryId).FirstOrDefault().CategoryId);
            productsDtos.ForEach(x => x.CategoryName = categories.Where(y => y.Id == x.CategoryId).FirstOrDefault().Name);

            return Ok(productsDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            if (id is null)
                return NotFound();

            var products = await _productRepository.GetAllAsync();

            if (products.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");            

            var product = await _productRepository.GetAsync(id);

            if (product is null)
                return NotFound("Bele product movcud deyil");

            var productDto = _mapper.Map<ProductDto>(product);

            //subCategoryDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/category/" + subCategoryDto.ImageName;

            var subCategories = await _subCategoryRepository.GetAllAsync();
            productDto.SubCategoryName = subCategories.Where(y => y.Id == productDto.SubCategoryId).FirstOrDefault().Name;

            var brands = await _brandRepository.GetAllAsync();
            productDto.BrandName = brands.Where(y => y.Id == productDto.BrandId).FirstOrDefault().Name;

            var categories = await _categoryRepository.GetAllAsync();
            productDto.CategoryId= subCategories.Where(y => y.Id == productDto.SubCategoryId).FirstOrDefault().CategoryId;
            productDto.CategoryName = categories.Where(y => y.Id == productDto.CategoryId).FirstOrDefault().Name;

            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "SubCategory");

            //subCategoryCreateDto.ImageName = await subCategoryCreateDto.Image.GenerateFile(forderPath);

            var product = _mapper.Map<Product>(productCreateDto);

            await _productService.AddAsync(product);

            return Created(HttpContext.Request.Path, product.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] ProductUpdateDto productUpdateDto)
        {
            var products = await _productRepository.GetAllAsync();

            if (products.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");

            await _productService.UpdateById(id, productUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _productService.CompletelyDeleteAsync(id);

            return Ok();
        }        

    }
}
