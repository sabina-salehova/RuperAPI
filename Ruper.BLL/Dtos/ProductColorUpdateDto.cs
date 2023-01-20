using Microsoft.AspNetCore.Http;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class ProductColorUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string? SKU { get; }
        public int? Quantity { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
