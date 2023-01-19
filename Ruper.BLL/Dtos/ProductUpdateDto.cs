using Microsoft.AspNetCore.Http;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public double? DisCount { get; set; }
        public double? Rate { get; set; }
        public bool? IsDeleted { get; set; }
        public int? BrandId { get; set; }
        public int? SubCategoryId { get; set; }
    }
}
