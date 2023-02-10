using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        public int ProductColorId { get; set; }
        public string? UserId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
