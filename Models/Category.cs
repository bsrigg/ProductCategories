using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { set; get; }
        public string Name { get; set; }
        public List<Widget> CategoryGrouping { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


    }
}