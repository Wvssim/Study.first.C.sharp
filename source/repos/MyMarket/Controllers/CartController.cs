using Microsoft.AspNetCore.Mvc;
using MyMarket.Data;
using MyMarket.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMarket.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        // 🧺 Panier temporaire stocké en mémoire
        private static List<Product> cartItems = new List<Product>();

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Afficher le contenu du panier
        public IActionResult Index()
        {
            return View(cartItems);
        }

        // ✅ Ajouter un produit à partir de la base
        [HttpPost, HttpGet]
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                cartItems.Add(product);
            }

            return RedirectToAction("Index");
        }


        // ✅ Supprimer un produit du panier
        public IActionResult RemoveFromCart(int id)
        {
            var product = cartItems.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                cartItems.Remove(product);
            }

            return RedirectToAction("Index");
        }

        // ✅ Vider le panier
        public IActionResult ClearCart()
        {
            cartItems.Clear();
            return RedirectToAction("Index");
        }
    }
}
