using Microsoft.AspNetCore.Mvc;
using MyMarket.Data;
using MyMarket.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace MyMarket.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", user.Role);
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User newUser)
        {
            if (!ModelState.IsValid)
                return View(newUser);

            // Vérifie si un utilisateur avec le même email existe déjà
            if (_context.Users.Any(u => u.Email == newUser.Email))
            {
                ViewBag.Error = "Un compte avec cet email existe déjà.";
                return View(newUser);
            }

            // Sauvegarde dans la base
            _context.Users.Add(newUser);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "✅ Compte créé avec succès ! Vous pouvez maintenant vous connecter.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
