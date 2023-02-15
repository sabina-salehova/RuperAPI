using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Color> _colorRepository;
        private readonly IColorService _colorService;

        public ColorsController(IMapper mapper, IRepository<Color> colorRepository, IColorService colorService)
        {
            _mapper = mapper;
            _colorRepository = colorRepository;
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colors = await _colorRepository.GetAllAsync();

            if (colors.Count == 0)
                return NotFound("Hele hec bir color yaradilmayib");

            var colorsDtos = _mapper.Map<List<ColorDto>>(colors);

            return Ok(colorsDtos);
        }

        [HttpGet("isNotDeleted")]
        public async Task<IActionResult> GetIsNotDeleted()
        {
            var colors = await _colorRepository.GetAllIsNotDeletedAsync();

            if (colors.Count == 0)
                return NotFound("Hele hec bir delete olmayan color yaradilmayib");

            var colorsDtos = _mapper.Map<List<ColorDto>>(colors);

            return Ok(colorsDtos);
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute] int? id)
        {
            var colors = await _colorRepository.GetAllAsync();

            if (colors.Count == 0)
                return NotFound("Hele hec bir color yaradilmayib");

            if (id is null)
                return NotFound();

            var color = await _colorRepository.GetAsync(id);

            if (color is null)
                return NotFound("Bele color movcud deyil");

            var colorDto = _mapper.Map<ColorDto>(color);

            return Ok(colorDto);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Post([FromForm] ColorCreateDto colorCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var colorCategory = _mapper.Map<Color>(colorCreateDto);

            await _colorService.AddAsync(colorCategory);

            return Created(HttpContext.Request.Path, colorCategory.Id);
        }

        [HttpPut("{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Put([FromRoute] int? id, [FromForm] ColorUpdateDto colorUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var colors = await _colorRepository.GetAllAsync();

            if (colors.Count == 0)
                return NotFound("Hele hec bir color yaradilmayib");

            await _colorService.UpdateById(id, colorUpdateDto);

            return Ok();
        }

        [HttpDelete("completelyDelete/{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _colorService.CompletelyDeleteAsync(id);

            return Ok();
        }

    }
}
