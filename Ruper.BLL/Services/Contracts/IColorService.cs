using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.BLL.Services.Contracts
{
    public interface IColorService : IRepository<Color>
    {
        Task UpdateById(int? id, ColorUpdateDto colorUpdateDto);
    }
}
