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
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<ProductColor> _productColorRepository;
        public OrdersController(IMapper mapper, UserManager<ApplicationUser> userManager, IRepository<Order> orderRepository, IRepository<ProductColor> productColorRepository, IOrderService orderService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _productColorRepository = productColorRepository;
            _orderService = orderService;
        }

        [HttpPost("OrderByUser")]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] OrderWithoutUserDto orderCreateDto)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (orderCreateDto.Quantity<0)
                    return BadRequest();

                var orderDto = _mapper.Map<OrderCreateDto>(orderCreateDto);

                string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                orderDto.UserId = user.Id;

                var order = _mapper.Map<Order>(orderDto);

                await _orderService.AddAsync(order);

                return Created(HttpContext.Request.Path, order.Id);

            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderRepository.GetAllAsync();

            if (orders.Count == 0)
                return NotFound("Hele hec bir order yaradilmayib");

            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);

            var users = await _userManager.Users.ToListAsync();
            ordersDtos.ForEach(x => x.UserName = users.Where(y => y.Id == x.UserId).FirstOrDefault().UserName);

            return Ok(ordersDtos);
        }

        [HttpDelete("completelyDeleteByAdministrator/{id?}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CompletelyDelete([FromRoute] int? id)
        {
            await _orderRepository.CompletelyDeleteAsync(id);
            return Ok();
        }
    }
}
