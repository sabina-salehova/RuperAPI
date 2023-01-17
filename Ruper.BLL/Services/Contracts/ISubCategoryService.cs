using Ruper.BLL.Dtos;
using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;

namespace Ruper.BLL.Services.Contracts
{
    public interface ISubCategoryService : IRepository<SubCategory>
    {
        Task UpdateById(int? id, SubCategoryUpdateDto subCategoryUpdateDto);
    }
}
