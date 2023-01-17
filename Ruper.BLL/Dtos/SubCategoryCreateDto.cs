using Microsoft.AspNetCore.Http;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class SubCategoryCreateDto
    {
        public string Name { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName = string.Empty;

        public bool IsDeleted = false;
        public int CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
        //public Category Category { get;}
        //public List<Category>? AllCategories { get; }

        //public DateTime CreatedAt = DateTime.Now;

        //public string CreatedBy = "Admin";
    }
}
