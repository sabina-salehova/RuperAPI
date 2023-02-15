using System.ComponentModel.DataAnnotations;

namespace Ruper.BLL.Dtos
{
    public class GeneralRatingDto
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public string? UserName { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Rate { get; set; }
        public string? Message { get; set; }
    }
}
