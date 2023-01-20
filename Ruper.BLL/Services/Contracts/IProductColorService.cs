using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.BLL.Services.Contracts
{
    public interface IProductColorService : IRepository<ProductColor>
    {
        Task UpdateById(int? id, SubCategoryUpdateDto subCategoryUpdateDto); 
    }
}
