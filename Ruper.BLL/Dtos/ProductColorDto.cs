using System.ComponentModel.DataAnnotations;

namespace Ruper.BLL.Dtos
{
    public class ProductColorDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Required]
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public bool IsDeleted { get; set; }
        public List<GeneralPCIDto>? GeneralProductColorImages { get; set; } = new List<GeneralPCIDto>();

    }
}
