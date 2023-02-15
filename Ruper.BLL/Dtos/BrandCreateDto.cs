using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class BrandCreateDto
    {
        public string Name { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName = string.Empty;

        public bool IsDeleted = false;
    }
}
