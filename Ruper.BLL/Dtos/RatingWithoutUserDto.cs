using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class RatingWithoutUserDto
    {        

        [Required]
        public int ProductId { get; set; }

        [Required]
        public double Rate { get; set; }
        public string? Message { get; set; }
    }
}
