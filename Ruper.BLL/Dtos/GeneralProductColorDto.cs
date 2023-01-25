using Microsoft.AspNetCore.Mvc;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class GeneralProductColorDto
    {
        public int Id { get; set; }        
        public string? SKU { get; set; }
        public int? Quantity { get; set; }
        public int? ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
        public bool? IsDeleted { get; set; }
        public List<GeneralPCIDto>? GeneralProductColorImages { get; set; } = new List<GeneralPCIDto>();

    }
}
