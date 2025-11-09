using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using System.Collections.Generic;

namespace MyShop.Controllers
{
    public class ProductsController : Controller
    {
        // Liste de produits en mémoire (nom corrigé en minuscule)
        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "AirPods 3", Description = "AirPods 3ème génération", Price = 169, Stock = 10 },
            new Product { Id = 2, Name = "Clavier", Description = "Clavier mécanique", Price = 120, Stock = 5 }
        };

        // GET: /Products
        public IActionResult Index()
        {
            return View(products);
        }

        // GET: /Products/Details/1
        public IActionResult Details(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: /Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = products.Count + 1;
                products.Add(product);
                return RedirectToAction("Dashboard", "Account");
            }
            return View(product);
        }
    }
}