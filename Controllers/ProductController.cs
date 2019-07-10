using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsCategories.Models;


namespace ProductsCategories.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext context;

        public ProductController(ProductContext pc)
        {
            context = pc;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Products()
        {
            ViewBag.p = context.Products;
            return View();
        }
        [Route("createproduct")]
        [HttpPost]
        public IActionResult CreateProduct(Product p)
        {
            if (ModelState.IsValid)
            {
                context.Create(p);
            }
            return Redirect("/");
        }
        [Route("createcategory")]
        [HttpPost]
        public IActionResult CreateCategory(Category c)
        {
            if (ModelState.IsValid)
            {
                context.Create(c);
            }
            return Redirect("/categories");
        }
        [Route("/categories")]
        [HttpGet]
        public IActionResult Categories()
        {
            ViewBag.c = context.Categories;
            return View();
        }
        [Route("/products/{id}")]
        [HttpGet]
        public IActionResult ProductAssoc(int id)
        {
            var MyProduct = context.Products.Include(p => p.ProductGrouping).ThenInclude(w =>w.Category).FirstOrDefault(p => p.ProductId == id);
            ViewBag.MyProduct = MyProduct;
            ViewBag.MyCategories=context.Categories;
            ViewBag.CurrentCategories=Dedupe(context.Categories.ToList(), MyProduct.ProductGrouping.ToList());
            return View();
        }
        [Route("CreateProdAssoc")]
        [HttpPost]
        public IActionResult CreateProdAssoc(Widget w)
        {
            context.Create(w);
            return Redirect($"/products/{w.ProductId}");
        }
        [Route("CreateCatAssoc")]
        [HttpPost]
        public IActionResult CreateCatAssoc(Widget w)
        {
            context.Create(w);
            return Redirect($"/categories/{w.CategoryId}");
        }
        [Route("/categories/{id}")]
        [HttpGet]
        public IActionResult CategoryAssoc(int id)
        {
            var MyCategory = context.Categories.Include(p => p.CategoryGrouping).ThenInclude(w => w.Product).FirstOrDefault(p => p.CategoryId == id);
            ViewBag.MyCategory = MyCategory;
            ViewBag.MyProducts = context.Products;
            ViewBag.CurrentProducts = DeProd(context.Products.ToList(), MyCategory.CategoryGrouping.ToList());
            return View();
        }
        public static List<Product> DeProd(List<Product> AllProducts, List<Widget> CurrProduct)
        {
            List<Product> NewProdList = new List<Product>();
            foreach (var a in AllProducts)
            {
                bool found = false;
                foreach (var c in CurrProduct)
                {
                    if (a.ProductId == c.Product.ProductId)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    NewProdList.Add(a);
                }
            }
            return NewProdList;
        }
        public static List<Category> Dedupe(List<Category> AllCategories, List<Widget> CurrCategory)
        {
            List<Category>NewList = new List<Category>();
            foreach(var a in AllCategories)
            {
                bool found = false;
                foreach(var c in CurrCategory)
                {
                    if(a.CategoryId == c.Category.CategoryId)
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    NewList.Add(a);
                }
            }
            return NewList;
        }
        [Route("UnCategorize/{ProductId}/{CategoryId}")]
        [HttpGet]
        public IActionResult UnCategorize(int ProductId, int CategoryId)
        {
            context.Remove(ProductId,CategoryId);
            return Redirect($"/products/{ProductId}");
        }
    }
}
