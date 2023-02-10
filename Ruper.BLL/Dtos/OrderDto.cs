using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public string? UserName { get; set; }

        [Required]
        public int ProductColortId { get; set; }

        [Required]
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public double? DisCount { get; set; }
        public double? TotalPrice { get; set; }
    }
}
