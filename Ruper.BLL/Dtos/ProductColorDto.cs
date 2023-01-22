﻿using Ruper.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruper.BLL.Dtos
{
    public class ProductColorDto
    {
        public int Id { get; set; }
        public string SKU { get; }
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Required]
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
