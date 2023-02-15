using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class GeneralProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
        public double DisCount { get; set; }
        public double Rate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ImageName { get; set; }=string.Empty;
        public List<GeneralProductColorDto>? GeneralProductColors { get; set; }=new List<GeneralProductColorDto>();
        public List<GeneralRatingDto>? ProductRatings { get; set; } = new List<GeneralRatingDto>();
    }
}
