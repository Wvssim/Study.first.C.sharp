using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using System.Collections.Generic;

namespace MyShop.Controllers
{
    public class AccountController : Controller
    {
        // Liste temporaire de produits (pour tester le dashboard)
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "AirPods 3", Description = "AirPods 3ème génération", Price = 169, Stock = 10 },
            new Product { Id = 2, Name = "Clavier", Description = "Clavier mécanique", Price = 120, Stock = 5 }
        };

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                // Ici tu pourrais vérifier email/password dans la DB
                ViewBag.Message = "Connexion réussie !";
                return RedirectToAction("Dashboard"); // redirige vers le dashboard après login
            }
            else
            {
                ViewBag.Message = "Veuillez remplir tous les champs.";
            }
            return View();
        }

        // GET: /Account/Dashboard
        [HttpGet]
        public IActionResult Dashboard()
        {
            // Pour l'instant on envoie la liste statique de produits
            return View(products);
        }
    }
}
