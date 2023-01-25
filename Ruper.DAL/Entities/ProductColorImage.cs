using Ruper.DAL.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.DAL.Entities
{
    public class ProductColorImage : TimeStample, IEntity
    {        
        public int Id { get; set; }
        public string ImageName { get; set; }

        [Required]
        public int ProductColorId { get; set; }
        public virtual ProductColor ProductColor { get; set; }
        public bool IsDeleted { get; set; }
    }
}
