using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class RatingDto
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public string? UserName { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }

        [Required]
        public double Rate { get; set; }
        public string? Message { get; set; }
    }
}
