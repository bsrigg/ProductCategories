using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<Widget> ProductGrouping { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}