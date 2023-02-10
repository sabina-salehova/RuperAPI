using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ruper.BLL.Dtos;
using Ruper.BLL.Services.Contracts;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;
using System.Security.Claims;

namespace Ruper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IMapper _mapper;
        private readonly IRatingService _ratingService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Product> _productRepository;
        public RatingsController(IMapper mapper, UserManager<ApplicationUser> userManager, IRepository<Rating> ratingRepository, IRepository<Product> productRepository, IRatingService ratingService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _ratingRepository = ratingRepository;
            _productRepository = productRepository;
            _ratingService = ratingService;
        }

        [HttpPost("Rate")]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] RatingWithoutUserDto ratingCreateDto)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (ratingCreateDto.Rate > 5 || ratingCreateDto.Rate < 0)
                    return BadRequest(ModelState);

                var ratingDto = _mapper.Map<RatingCreateDto>(ratingCreateDto);

                string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                ratingDto.UserId = user.Id;

                var rating = _mapper.Map<Rating>(ratingDto);

                await _ratingService.AddAsync(rating);

                return Created(HttpContext.Request.Path, rating.Id);

            }
            return BadRequest();
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            var ratings = await _ratingRepository.GetAllAsync();

            if (ratings.Count == 0)
                return NotFound("Hele hec bir rating yaradilmayib");

            var ratingsDtos = _mapper.Map<List<RatingDto>>(ratings);

            var products = await _productRepository.GetAllAsync();
            ratingsDtos.ForEach(x => x.ProductName = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Name);

            var users = await _userManager.Users.ToListAsync();
            ratingsDtos.ForEach(x => x.UserName = users.Where(y => y.Id == x.UserId).FirstOrDefault().UserName);

            return Ok(ratingsDtos);
        }
    }
}
