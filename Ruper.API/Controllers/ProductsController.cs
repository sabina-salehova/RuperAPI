using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ruper.AuthenticationService.Services.Contracts;
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
        private readonly IRepository<ProductColor> _productColorRepository;
        private readonly IRepository<Color> _ColorRepository;
        private readonly IRepository<ProductColorImage> _productColorImageRepository;
        private readonly IRepository<Rating> _ratingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductsController(IMapper mapper, IRepository<SubCategory> subCategoryRepository, IRepository<Category> categoryRepository, IRepository<Brand> brandRepository, IRepository<Product> productRepository, IProductService productService, IRepository<ProductColor> productColorRepository, IRepository<Color> colorRepository, IRepository<ProductColorImage> productColorImageRepository, IRepository<Rating> ratingRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _productService = productService;
            _productColorRepository = productColorRepository;
            _ColorRepository = colorRepository;
            _productColorImageRepository = productColorImageRepository;
            _ratingRepository = ratingRepository;
            _userManager = userManager;
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
                               .ForEach(y => y.GeneralProductColorImages.ForEach(z => z.ImageName = imagePath + z.ImageName)));

            List<string> images;
            
            generalProductsDtos.ForEach(x => {
                images = new List<string>();
                x.GeneralProductColors
                .ForEach(y => y.GeneralProductColorImages
                .ForEach(z => images.Add(z.ImageName)));
                x.ImageName = images.FirstOrDefault();
            });

            var colors = await _ColorRepository.GetAllAsync();

            generalProductsDtos.ForEach(x => x.GeneralProductColors.ForEach(y => y.ColorName = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorName));
            generalProductsDtos.ForEach(x => x.GeneralProductColors.ForEach(y => y.ColorCode = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorCode));

            var ratings = await _ratingRepository.GetAllAsync();

            generalProductsDtos
                .ForEach(x => x.ProductRatings = _mapper.
                Map<List<GeneralRatingDto>>(ratings.
                Where(y => y.ProductId == x.Id && !x.IsDeleted).
                ToList()));

            List<ApplicationUser> users = _userManager.Users.ToList();

            generalProductsDtos
                .ForEach(z=>z.ProductRatings
                .ForEach(x => x.UserName = users
                .Where(y => y.Id == x.UserId)
                .FirstOrDefault().UserName));

            return Ok(generalProductsDtos);
        }

        [HttpGet("withoutColorsAndImages")]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAllAsync();

            if (products.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");

            var productsDtos = _mapper.Map<List<ProductDto>>(products);

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

            var productDto = _mapper.Map<GeneralProductDto>(product);

            var subCategories = await _subCategoryRepository.GetAllAsync();
            productDto.SubCategoryName = subCategories.Where(y => y.Id == productDto.SubCategoryId).FirstOrDefault().Name;

            var brands = await _brandRepository.GetAllAsync();
            productDto.BrandName = brands.Where(y => y.Id == productDto.BrandId).FirstOrDefault().Name;

            var categories = await _categoryRepository.GetAllAsync();
            productDto.CategoryId = subCategories.Where(y => y.Id == productDto.SubCategoryId).FirstOrDefault().CategoryId;
            productDto.CategoryName = categories.Where(y => y.Id == productDto.CategoryId).FirstOrDefault().Name;

            var productColors = await _productColorRepository.GetAllIsNotDeletedAsync();

            productDto.GeneralProductColors = _mapper.Map<List<GeneralProductColorDto>>(productColors.Where(y => y.ProductId == productDto.Id && !productDto.IsDeleted).ToList());

            var productColorImages = await _productColorImageRepository.GetAllIsNotDeletedAsync();

            productDto.GeneralProductColors
                               .ForEach(y => y.GeneralProductColorImages = _mapper.Map<List<GeneralPCIDto>>(productColorImages.Where(z => z.ProductColorId == y.Id).ToList()));

            string imagePath = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/";

            productDto.GeneralProductColors
                               .ForEach(y => y.GeneralProductColorImages.ForEach(z => z.ImageName = imagePath + z.ImageName));

            var colors = await _ColorRepository.GetAllAsync();

            productDto.GeneralProductColors.ForEach(y => y.ColorName = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorName);
            productDto.GeneralProductColors.ForEach(y => y.ColorCode = colors.Where(x => x.Id == y.ColorId).FirstOrDefault().ColorCode);

            var ratings = await _ratingRepository.GetAllAsync();

            productDto.ProductRatings= _mapper.
                Map<List<GeneralRatingDto>>(ratings.
                Where(y => y.ProductId == productDto.Id && !y.IsDeleted).
                ToList());

            List<ApplicationUser> users = _userManager.Users.ToList();

            productDto.ProductRatings
                .ForEach(x => x.UserName = users
                .Where(y => y.Id == x.UserId)
                .FirstOrDefault().UserName);

            List<string> images= new List<string>(); ;

            productDto.GeneralProductColors
                .ForEach(y => y.GeneralProductColorImages
                .ForEach(z => images.Add(z.ImageName)));

            productDto.ImageName = images.FirstOrDefault();

            return Ok(productDto);
        }

        [HttpGet("withoutColorsAndImages/{id?}")]
        public async Task<IActionResult> WithoutColorsAndImagesGet([FromRoute] int? id)
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Post([FromForm] ProductCreateDto productCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(productCreateDto);

            await _productService.AddAsync(product);

            return Created(HttpContext.Request.Path, product.Id);
        }

        [HttpPut("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] ProductUpdateDto productUpdateDto)
        {
            var products = await _productRepository.GetAllAsync();

            if (products.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");

            await _productService.UpdateById(id, productUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _productService.CompletelyDeleteAsync(id);

            return Ok();
        }        

    }
}
