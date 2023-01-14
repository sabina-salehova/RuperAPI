using Ruper.DAL.Entities;
using Ruper.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.BLL.Services.Contracts
{
    public interface ICategoryService : IRepository<Category>
    {
        //Task<string> Test();
    }
}
