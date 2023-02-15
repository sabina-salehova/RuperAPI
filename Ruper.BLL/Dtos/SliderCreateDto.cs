using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class SliderCreateDto
    {            
        public string Title { get; set; }
        public string Subtitle { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName = string.Empty;
        public string ButtonName { get; set; }
        public string ButtonLink { get; set; }

        public bool IsDeleted = false;
    }
}
