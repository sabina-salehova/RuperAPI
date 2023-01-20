using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class ProductColorCreateDto
    {
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ColorId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
