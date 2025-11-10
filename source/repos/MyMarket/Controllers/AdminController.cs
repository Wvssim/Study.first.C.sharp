using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyMarket.Data;
using MyMarket.Models;
using System.Linq;

namespace MyMarket.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ PAGE LOGIN
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("isLogged", "true");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Nom d'utilisateur ou mot de passe incorrect";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("isLogged");
            return RedirectToAction("Login");
        }

        // ✅ DASHBOARD — produits depuis la base SQL
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("isLogged") != "true")
                return RedirectToAction("Login");

            var products = _context.Products.ToList();
            return View(products);
        }

        // ✅ AJOUT PRODUIT
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("isLogged") != "true")
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (HttpContext.Session.GetString("isLogged") != "true")
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges(); // ✅ Enregistre dans la base
                return RedirectToAction("Dashboard");
            }
            return View(product);
        }

        // ✅ MODIFIER PRODUIT
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("isLogged") != "true")
                return RedirectToAction("Login");

            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (HttpContext.Session.GetString("isLogged") != "true")
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(product);
        }

        // ✅ SUPPRIMER PRODUIT
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("isLogged") != "true")
                return RedirectToAction("Login");

            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
