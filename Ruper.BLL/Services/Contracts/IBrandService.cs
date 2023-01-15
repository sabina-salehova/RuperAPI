using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.BLL.Services.Contracts
{
    public interface IBrandService : IRepository<Brand>
    {
        Task UpdateById(int? id, BrandUpdateDto brandUpdateDto);
    }
}
