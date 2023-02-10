using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
