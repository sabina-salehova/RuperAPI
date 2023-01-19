using Ruper.DAL.Base;
using System.ComponentModel.DataAnnotations;

namespace Ruper.DAL.Entities
{
    public class Color : TimeStample, IEntity
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
