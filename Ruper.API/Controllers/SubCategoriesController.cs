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
    public class SubCategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SubCategory> _subCategoryRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ISubCategoryService _subCategoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SubCategoriesController(IMapper mapper, IRepository<SubCategory> subCategoryRepository, ISubCategoryService subCategoryService, IWebHostEnvironment webHostEnvironment, IRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _subCategoryRepository = subCategoryRepository;
            _subCategoryService = subCategoryService;
            _webHostEnvironment = webHostEnvironment;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subCategories = await _subCategoryRepository.GetAllAsync();

            if (subCategories.Count == 0)
                return NotFound("Hele hec bir subCategory yaradilmayib");

            var subCategoriesDtos = _mapper.Map<List<SubCategoryDto>>(subCategories);            

            subCategoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/subcategory/" + x.ImageName);

            var categories = await _categoryRepository.GetAllAsync();

            subCategoriesDtos.ForEach(x => x.CategoryName = categories.Where(y=>y.Id==x.CategoryId).FirstOrDefault().Name);

            return Ok(subCategoriesDtos);
        }

        [HttpGet("isNotDeleted")]
        public async Task<IActionResult> GetIsActive()
        {
            var subCategories = await _subCategoryRepository.GetAllIsNotDeletedAsync();

            if (subCategories.Count == 0)
                return NotFound("Hele hec bir delete olmayan subCategory yaradilmayib");

            var subCategoriesDtos = _mapper.Map<List<SubCategoryDto>>(subCategories);
            
            subCategoriesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/subcategory/" + x.ImageName);

            var categories = await _categoryRepository.GetAllAsync();

            subCategoriesDtos.ForEach(x => x.CategoryName = categories.Where(y => y.Id == x.CategoryId).FirstOrDefault().Name);

            return Ok(subCategoriesDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            if (id is null)
                return NotFound();

            var subCategories = await _subCategoryRepository.GetAllAsync();

            if (subCategories.Count == 0)
                return NotFound("Hele hec bir subCategory yaradilmayib");

            var subCategory = await _subCategoryRepository.GetAsync(id);

            if (subCategory is null)
                return NotFound("Bele subCategory movcud deyil");

            var subCategoryDto = _mapper.Map<SubCategoryDto>(subCategory);

            subCategoryDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/category/" + subCategoryDto.ImageName;

            var categories = await _categoryRepository.GetAllAsync();

            subCategoryDto.CategoryName = categories.Where(y => y.Id == subCategoryDto.CategoryId).FirstOrDefault().Name;

            return Ok(subCategoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SubCategoryCreateDto subCategoryCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "SubCategory");

            subCategoryCreateDto.ImageName = await subCategoryCreateDto.Image.GenerateFile(forderPath);

            var subCategoriedCategory = _mapper.Map<SubCategory>(subCategoryCreateDto);

            await _subCategoryService.AddAsync(subCategoriedCategory);

            return Created(HttpContext.Request.Path, subCategoriedCategory.Id);
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] SubCategoryUpdateDto subCategoryUpdateDto)
        {    
            var subCategories = await _subCategoryRepository.GetAllAsync();

            if (subCategories.Count == 0)
                return NotFound("Hele hec bir subCategory yaradilmayib");

            await _subCategoryService.UpdateById(id, subCategoryUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _subCategoryService.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
