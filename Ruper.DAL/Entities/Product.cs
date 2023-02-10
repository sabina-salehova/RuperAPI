using Ruper.DAL.Base;
using System.ComponentModel.DataAnnotations;

namespace Ruper.DAL.Entities
{
    public class Product : TimeStample, IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }
        public double? DisCount { get; set; }
        public double? Rate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
        
        [Required]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual List<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
