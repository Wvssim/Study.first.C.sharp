using Microsoft.AspNetCore.Mvc;
using MyMarket.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMarket.Controllers
{
    public class CartController : Controller
    {
        // 🔹 Simulation d'une "liste de produits disponibles"
        private static List<Product> allProducts = new List<Product>
        {
            new Product { Id = 1, Name = "AirPods Pro 2", Description = "Écouteurs sans fil", Price = 1500, Stock = 10 },
            new Product { Id = 2, Name = "iPhone 15", Description = "Smartphone Apple", Price = 12000, Stock = 5 },
            new Product { Id = 3, Name = "MacBook Air M3", Description = "Ordinateur portable", Price = 14500, Stock = 4 }
        };

        // 🔹 Panier en mémoire (temporaire)
        private static List<Product> cartItems = new List<Product>();

        // ✅ Afficher le contenu du panier
        public IActionResult Index()
        {
            return View(cartItems);
        }

        // ✅ Ajouter un produit au panier
        public IActionResult AddToCart(int id)
        {
            var product = allProducts.FirstOrDefault(p => p.Id == id);
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
