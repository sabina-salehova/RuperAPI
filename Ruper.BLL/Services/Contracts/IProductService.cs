using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.BLL.Services.Contracts
{
    public interface IProductService : IRepository<Product>
    {
        Task UpdateById(int? id, ProductUpdateDto productUpdateDto);
    }
}
