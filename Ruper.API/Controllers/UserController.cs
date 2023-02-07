using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Ruper.AuthenticationService.Models;
using Ruper.AuthenticationService.Services.Contracts;
using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using System.Security.Claims;
using static Ruper.DAL.Constants.Authorization;

namespace Ruper.API.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserService userService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {

            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }
        
        [HttpPost("addrole")]
        [Authorize(Roles = "Administator")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }

        [HttpGet("All")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users.Count == 0)
                return NotFound();

            var userDtos = _mapper.Map<List<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("getUserByToken")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUserIdByToken()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return Ok(user.Id);
            }

            return BadRequest();
        }

    }
}
