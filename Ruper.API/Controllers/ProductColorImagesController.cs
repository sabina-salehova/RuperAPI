using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ruper.BLL.Data;
using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorImagesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<ProductColor> _productColorRepository;
        private readonly IRepository<ProductColorImage> _productColorImageRepository;


        public ProductColorImagesController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IRepository<ProductColor> productColorRepository, IRepository<ProductColorImage> productColorImageRepository)
        {
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _productColorRepository = productColorRepository;
            _productColorImageRepository = productColorImageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productImages = await _productColorImageRepository.GetAllAsync();

            if (productImages.Count == 0)
                return NotFound("Hele hec bir product image yaradilmayib");

            var productColorImagesDtos = _mapper.Map<List<GeneralPCIDto>>(productImages);

            productColorImagesDtos.ForEach(x => x.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/" + x.ImageName);

            return Ok(productColorImagesDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            var images = await _productColorImageRepository.GetAllAsync();

            if (images.Count == 0)
                return NotFound("Hele hec bir product image yaradilmayib");

            if (id is null)
                return NotFound();

            var image = await _productColorImageRepository.GetAsync(id);

            if (image is null)
                return NotFound("Bele product image movcud deyil");

            var imageDto = _mapper.Map<GeneralPCIDto>(image);

            imageDto.ImageName = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/images/product/" + imageDto.ImageName;

            return Ok(imageDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int? productColorId, [FromForm] List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productColorId is null)
                return BadRequest(ModelState);

            var productColor = await _productColorRepository.GetAsync(productColorId);

            if (productColor is null)
                return BadRequest(ModelState);

            var forderPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "Product");
            string newImage;
            List<PCICreateDto> images = new List<PCICreateDto>();

            foreach (var image in files)
            {              
                newImage = await image.GenerateFile(forderPath);
                images.Add(new PCICreateDto { ImageName=newImage.ToString(), ProductColorId=(int)productColorId});
            }

            List<ProductColorImage> createdProductColorImages = _mapper.Map<List<PCICreateDto>, List<ProductColorImage>>(images);

            await _productColorImageRepository.AddAsync(createdProductColorImages);

            return Created(HttpContext.Request.Path, "ok");
        }

        [HttpDelete("completelyDelete/{id?}")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _productColorImageRepository.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
