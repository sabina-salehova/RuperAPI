using Ruper.DAL.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruper.DAL.Entities
{
    public class ProductColor : TimeStample, IEntity
    {        
        public int Id { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<ProductColorImage> ProductColorImages { get; set; } = new List<ProductColorImage>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}
