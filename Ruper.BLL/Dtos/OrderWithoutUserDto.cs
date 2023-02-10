using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class OrderWithoutUserDto
    {  
        [Required]
        public int ProductColorId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
