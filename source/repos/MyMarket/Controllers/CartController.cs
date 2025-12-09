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

        // 🧺 Panier stocké en mémoire (simple mais OK pour maintenant)
        private static List<CartItem> cartItems = new List<CartItem>();

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🛒 Affichage du panier
        public IActionResult Index()
        {
            return View(cartItems);
        }

        // ➕ Ajouter un produit (avec gestion de quantité)
        [HttpPost, HttpGet]
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var item = cartItems.FirstOrDefault(c => c.Product.Id == id);

                if (item == null)
                {
                    cartItems.Add(new CartItem { Product = product, Quantity = 1 });
                }
                else
                {
                    item.Quantity++;
                }
            }

            return RedirectToAction("Index");
        }

        // ❌ Retirer 1 quantité
        public IActionResult RemoveOne(int id)
        {
            var item = cartItems.FirstOrDefault(c => c.Product.Id == id);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    cartItems.Remove(item);
                }
            }

            return RedirectToAction("Index");
        }

        // ♻ Supprimer complètement l'article
        public IActionResult RemoveFromCart(int id)
        {
            var item = cartItems.FirstOrDefault(c => c.Product.Id == id);

            if (item != null)
                cartItems.Remove(item);

            return RedirectToAction("Index");
        }

        // ❌ Vider le panier
        public IActionResult ClearCart()
        {
            cartItems.Clear();
            return RedirectToAction("Index");
        }

        // 📦 PASSER COMMANDE (Checkout)
        [HttpPost]
        public IActionResult Checkout()
        {
            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Votre panier est vide.";
                return RedirectToAction("Index");
            }

            // 🧾 Créer une commande
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(i => i.Product.Price * i.Quantity),
                Status = "Pending",
                UserEmail = "client@test.com",
                UserFullName = "Client",
                UserPhone = "0000000000"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // ➕ Ajouter les OrderItem
            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();

            // 🧹 Vider le panier
            cartItems.Clear();

            return RedirectToAction("OrderSuccess", new { id = order.Id });
        }

        // 🎉 Page de succès
        public IActionResult OrderSuccess(int id)
        {
            return View(id);
        }
    }
}
