using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.BLL.Services.Contracts
{
    public interface ICategoryService : IRepository<Category>
    {
        Task UpdateById(int? id, CategoryUpdateDto categoryUpdateDto);
    }
}
