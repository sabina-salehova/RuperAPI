using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class PCICreateDto
    {
        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName = string.Empty;

        public bool IsDeleted = false;

        [Required]
        public int ProductColorId { get; set; }

        //public DateTime CreatedAt = DateTime.Now;

        //public string CreatedBy = "Admin";
    }
}
