using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class ColorUpdateDto
    {
        public int Id { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
