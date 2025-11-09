using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using System.Collections.Generic;

namespace MyShop.Controllers
{
    public class CartController : Controller
    {
        // Panier en mémoire
        private static List<Product> cartItems = new List<Product>();

        // GET: /Cart
        public IActionResult Index()
        {
            return View(cartItems);
        }

        // Ajouter un produit au panier
        public IActionResult AddToCart(int id)
        {
            var product = ProductsController.products.Find(p => p.Id == id);
            if (product != null)
            {
                cartItems.Add(product);
            }
            return RedirectToAction("Index", "Products"); // retour à la liste des produits
        }

        // Supprimer un produit du panier
        public IActionResult RemoveFromCart(int id)
        {
            var product = cartItems.Find(p => p.Id == id);
            if (product != null)
            {
                cartItems.Remove(product);
            }
            return RedirectToAction("Index");
        }
    }
}
