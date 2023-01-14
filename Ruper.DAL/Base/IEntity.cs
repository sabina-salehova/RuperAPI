using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.DAL.Base
{
    public interface IEntity
    {
        int Id { get; set; }
    }
}
