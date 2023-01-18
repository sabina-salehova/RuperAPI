using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class ColorCreateDto
    {
        [Required]
        public string ColorName { get; set; }

        [Required]
        public string ColorCode { get; set; }

        public bool IsDeleted = false;
    }
}
