using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(IMapper mapper, IRepository<Category> categoryRepository, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories.Count == 0)
                return NotFound("Hele hec bir category yaradilmayib");

            var categoriesDtos = _mapper.Map<List<CategoryDto>>(categories);

            return Ok(categoriesDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            var categories = await _categoryRepository.GetAllAsync();

            if(categories.Count==0)
                return NotFound("Hele hec bir category yaradilmayib");

            if (id is null)
                return NotFound();

            var category = await _categoryRepository.GetAsync(id);

            if (category is null)
                return NotFound("Bele category movcud deyil");

            var categoryDto = _mapper.Map<Category>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CategoryCreateDto categoryCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            categoryCreateDto.ImageName = await SaveImage(categoryCreateDto.Image);

            var createdCategory = _mapper.Map<Category>(categoryCreateDto);

            await _categoryRepository.AddAsync(createdCategory);

            return Created(HttpContext.Request.Path, createdCategory.Id);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
                fileStream.Close();
            }

            return imageName;
        }

        [HttpPut("{id?}")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories.Count == 0)
                return NotFound("Hele hec bir category yaradilmayib");

            if (id is null) return NotFound();

            var existCategory = await _categoryRepository.GetAsync(id);

            if (existCategory is null) return NotFound();

            if (categoryUpdateDto.Id != id) return BadRequest();

            var category = _mapper.Map<Category>(categoryUpdateDto);

            await _categoryRepository.UpdateAsync(category);

            return Ok();
        }

        [HttpDelete("{id?}")]
        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
            await _categoryRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
