using System.ComponentModel.DataAnnotations;

namespace Ruper.BLL.Dtos
{
    public class RatingCreateDto
    {
        [Required]
        public int ProductId { get; set; }
        public string? UserId { get; set; }

        [Required]
        public double Rate { get; set; }
        public string? Message { get; set; }
    }
}
