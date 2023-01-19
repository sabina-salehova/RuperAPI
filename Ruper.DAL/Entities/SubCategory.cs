using Ruper.DAL.Base;
using System.ComponentModel.DataAnnotations;

namespace Ruper.DAL.Entities
{
    public class SubCategory : TimeStample, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
