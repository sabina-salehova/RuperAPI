using Microsoft.AspNetCore.Http;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class ProductCreateDto
    {
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

        [Required]
        public int SubCategoryId { get; set; }
    }
}
