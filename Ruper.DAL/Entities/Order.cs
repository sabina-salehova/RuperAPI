using Ruper.DAL.Base;
using System.ComponentModel.DataAnnotations;

namespace Ruper.DAL.Entities
{
    public class Order : TimeStample, IEntity
    {
        public int Id { get; set; }

        [Required]
        public int ProductColorId { get; set; }
        public virtual ProductColor ProductColor { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public double? DisCount { get; set; }
        public double? TotalPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
