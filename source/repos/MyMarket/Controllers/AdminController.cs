using Microsoft.AspNetCore.Mvc;
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

        // 🟢 [GET] Page de connexion admin
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 🟢 [POST] Connexion admin
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = _context.Users.FirstOrDefault(u =>
                u.Email == email &&
                u.Password == password &&
                u.Role == "Admin"
            );

            if (admin != null)
            {
                HttpContext.Session.SetString("UserEmail", admin.Email ?? "");
                HttpContext.Session.SetString("UserRole", admin.Role ?? "");

                return RedirectToAction("Dashboard");
            }

            ViewBag.ErrorMessage = "❌ Email ou mot de passe incorrect, ou vous n’êtes pas administrateur.";
            return View();
        }

        // 🔒 [GET] Tableau de bord admin
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Admin")
                return RedirectToAction("Login");

            var products = _context.Products.ToList();
            return View(products);
        }

        // 🟢 [GET] Ajouter un produit
        [HttpGet]
        public IActionResult Create()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Admin")
                return RedirectToAction("Login");

            return View("~/Views/Admin/Create.cshtml");
        }

        // 🟢 [POST] Ajouter un produit
        [HttpPost]
        public IActionResult Create(Product product)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Admin")
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View("~/Views/Admin/Create.cshtml", product);

            product.Category ??= "Autres";
            product.ImageUrl ??= "";

            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "✅ Produit ajouté avec succès !";
            return RedirectToAction("Dashboard");
        }

        // ✏️ [GET] Modifier un produit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Admin")
                return RedirectToAction("Login");

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View("~/Views/Admin/Edit.cshtml", product);
        }

        // ✏️ [POST] Modifier un produit
        [HttpPost]
        public IActionResult Edit(Product updatedProduct)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Admin")
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View("~/Views/Admin/Edit.cshtml", updatedProduct);

            var productInDb = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (productInDb == null)
                return NotFound();

            productInDb.Name = updatedProduct.Name;
            productInDb.Description = updatedProduct.Description;
            productInDb.Price = updatedProduct.Price;
            productInDb.Stock = updatedProduct.Stock;
            productInDb.Category = updatedProduct.Category;
            productInDb.ImageUrl = updatedProduct.ImageUrl;

            _context.SaveChanges();

            TempData["SuccessMessage"] = "✏️ Produit modifié avec succès !";
            return RedirectToAction("Dashboard");
        }

        // 👥 [GET] Gestion des utilisateurs
        public IActionResult ManageUsers()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Admin")
                return RedirectToAction("Login");

            var users = _context.Users.ToList();
            return View("~/Views/Admin/ManageUsers.cshtml", users);
        }

        // 🔄 [POST] Modifier le rôle d’un utilisateur
        [HttpPost]
        public IActionResult UpdateRole(int id, string role)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            user.Role = role;
            _context.SaveChanges();

            TempData["Success"] = "🔄 Rôle mis à jour avec succès !";
            return RedirectToAction("ManageUsers");
        }

        // 🗑️ [GET] Supprimer un utilisateur
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            TempData["Success"] = "🗑️ Utilisateur supprimé avec succès !";
            return RedirectToAction("ManageUsers");
        }

        // 🚪 Déconnexion
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
