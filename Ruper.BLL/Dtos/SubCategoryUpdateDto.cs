﻿using Microsoft.AspNetCore.Http;
using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class SubCategoryUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public string? ImageName = string.Empty;
        public bool? IsDeleted { get; set; }
        public int? CategoryId { get; set; }

        //public DateTime UpdatedAt = DateTime.Now;

        //public string UpdatedBy = "Admin";
    }
}