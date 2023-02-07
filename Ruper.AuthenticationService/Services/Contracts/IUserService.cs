using Ruper.AuthenticationService.Models;
using Ruper.DAL.Entities;
using System.Security.Claims;

namespace Ruper.AuthenticationService.Services.Contracts
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<IList<ApplicationUser>> GetAllAsync();
    }
}
