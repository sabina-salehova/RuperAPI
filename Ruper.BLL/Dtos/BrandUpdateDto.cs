using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class BrandUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public string? ImageName = string.Empty;
        public bool? IsDeleted { get; set; }
    }
}
