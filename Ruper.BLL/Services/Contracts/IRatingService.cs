using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.BLL.Services.Contracts
{
    public interface IRatingService : IRepository<Rating>
    {
        //Task UpdateById(int? id, ProductColorUpdateDto productColorUpdateDto); 
    }
}
