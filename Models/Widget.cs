using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsCategories.Models
{
    public class Widget
    {
        [Key]
        public int WidgetId { set; get; }
        public int ProductId { set; get; }
        public int CategoryId { set; get; }
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}