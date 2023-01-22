using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ruper.BLL.Data;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;
using System.Drawing;
using Color = Ruper.DAL.Entities.Color;

namespace Ruper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SubCategory> _subCategoryRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductColor> _productColorRepository;
        private readonly IRepository<Color> _ColorRepository;
        private readonly IProductColorService _productColorService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductColorsController(IMapper mapper, IRepository<SubCategory> subCategoryRepository, IWebHostEnvironment webHostEnvironment, IRepository<Category> categoryRepository, IRepository<Brand> brandRepository, IRepository<Product> productRepository, IRepository<ProductColor> productColorRepository, IProductColorService productColorService, IRepository<Color> colorRepository)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
            _productColorRepository = productColorRepository;
            _productColorService = productColorService;
            _ColorRepository = colorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productColors = await _productColorRepository.GetAllAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir productun coloru yaradilmayib");

            var productColorsDtos = _mapper.Map<List<ProductColorDto>>(productColors);

            //subCategoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/subcategory/" + x.ImageName);
            
            var products = await _productRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ProductName = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Name);

            var colors = await _ColorRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ColorName = colors.Where(y => y.Id == x.ColorId).FirstOrDefault().ColorName);

            return Ok(productColorsDtos);
        }

        [HttpGet("isNotDeleted")]
        public async Task<IActionResult> GetIsNotDeletede()
        {
            var productColors = await _productColorRepository.GetAllIsNotDeletedAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir delete olmayan productColor yaradilmayib");

            var productColorsDtos = _mapper.Map<List<ProductColorDto>>(productColors);

            //subCategoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/subcategory/" + x.ImageName);

            var products = await _productRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ProductName = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Name);

            var colors = await _ColorRepository.GetAllAsync();
            productColorsDtos.ForEach(x => x.ColorName = colors.Where(y => y.Id == x.ColorId).FirstOrDefault().ColorName);

            return Ok(productColorsDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            if (id is null)
                return NotFound();

            var productColors = await _productColorRepository.GetAllAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir productColor yaradilmayib");

            var productColor = await _productColorRepository.GetAsync(id);

            if (productColor is null)
                return NotFound("Bele productColor movcud deyil");

            var productColorDto = _mapper.Map<ProductColorDto>(productColor);

            //subCategoryDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/category/" + subCategoryDto.ImageName;

            var products = await _productRepository.GetAllAsync();
            productColorDto.ProductName = products.Where(y => y.Id == productColorDto.ProductId).FirstOrDefault().Name;
            
            var colors = await _ColorRepository.GetAllAsync();
            productColorDto.ColorName = colors.Where(y => y.Id == productColorDto.ColorId).FirstOrDefault().ColorName;

            return Ok(productColorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductColorCreateDto productColorCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "SubCategory");

            //subCategoryCreateDto.ImageName = await subCategoryCreateDto.Image.GenerateFile(forderPath);

            var productColor = _mapper.Map<ProductColor>(productColorCreateDto);

            var db = await _productColorRepository.GetAllAsync();

            int lastCount = db.ToList().Last().Id+1;

            productColor.SKU= "PRD-" + productColor.ProductId + "-CLR" + productColor.ColorId+ productColor.SKU+"-PCI" + lastCount;

            await _productColorService.AddAsync(productColor);
            
            return Created(HttpContext.Request.Path, productColor.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] ProductColorUpdateDto productColorUpdateDto)
        {
            var productColors = await _productColorRepository.GetAllAsync();

            if (productColors.Count == 0)
                return NotFound("Hele hec bir product yaradilmayib");

            await _productColorService.UpdateById(id, productColorUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _productColorService.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
