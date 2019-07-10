using Microsoft.EntityFrameworkCore;
using ProductsCategories.Models;
using System.Linq;

namespace ProductsCategories.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public void Create(Product p)
        {
            Add(p);
            SaveChanges();
        }
        public void Create(Category c)
        {
            Add(c);
            SaveChanges();
        }
        public void Create(Widget w)
        {
            Add(w);
            SaveChanges();
        }
        public Product GetProductById(int ProductId)
        {
            return Products.Where(p => p.ProductId == ProductId).FirstOrDefault();
        }
        public void Remove(int PId, int CId)
        {
            Widget Wid = Widgets.Where(w => w.ProductId == PId).Where(w => w.CategoryId == CId).FirstOrDefault();
            Remove(Wid);
            SaveChanges();
        }
        
    }
}