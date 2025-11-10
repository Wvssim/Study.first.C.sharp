using Microsoft.AspNetCore.Mvc;
using MyMarket.Data;
using System.Linq;

namespace MyMarket.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🛍️ Liste de tous les produits
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // 🔍 Détails d’un produit
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
