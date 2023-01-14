using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName = string.Empty;

        //public DateTime CreatedAt = DateTime.Now;

        //public string CreatedBy = "Admin";
    }
}
