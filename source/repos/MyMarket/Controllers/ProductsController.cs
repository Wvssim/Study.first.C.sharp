using Microsoft.AspNetCore.Mvc;
using MyMarket.Data;
using MyMarket.Models;
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

        // 🛍️ Liste de tous les produits avec filtres
        public IActionResult Index(string? search, string? category, decimal? minPrice, decimal? maxPrice)
        {
            var products = _context.Products.AsQueryable();

            // 🔍 Recherche
            if (!string.IsNullOrWhiteSpace(search))
                products = products.Where(p =>
                    p.Name.Contains(search) ||
                    p.Description.Contains(search));

            // 🏷️ Filtre Catégorie
            if (!string.IsNullOrWhiteSpace(category))
                products = products.Where(p => p.Category == category);

            // 💰 Prix min
            if (minPrice.HasValue)
                products = products.Where(p => p.Price >= minPrice.Value);

            // 💰 Prix max
            if (maxPrice.HasValue)
                products = products.Where(p => p.Price <= maxPrice.Value);

            // 🔄 Envoi des valeurs à la vue
            ViewData["search"] = search;
            ViewData["category"] = category;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;

            return View(products.ToList());
        }

        // 🔍 Détails d’un produit
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // ➕ [GET] Ajouter un produit
        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Create.cshtml");
        }

        // 💾 [POST] Ajouter un produit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Admin/Create.cshtml", product);

            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "✅ Produit ajouté avec succès !";
            return RedirectToAction("Index");
        }

        // ✏️ [GET] Modifier un produit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View("~/Views/Admin/Edit.cshtml", product);
        }

        // 💾 Modifier un produit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product updatedProduct)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Admin/Edit.cshtml", updatedProduct);

            var productInDb = _context.Products.FirstOrDefault(p => p.Id == updatedProduct.Id);
            if (productInDb == null)
                return NotFound();

            // 🔄 Mise à jour
            productInDb.Name = updatedProduct.Name;
            productInDb.Description = updatedProduct.Description;
            productInDb.Price = updatedProduct.Price;
            productInDb.Stock = updatedProduct.Stock;
            productInDb.Category = updatedProduct.Category;
            productInDb.ImageUrl = updatedProduct.ImageUrl;

            _context.SaveChanges();

            TempData["SuccessMessage"] = "✅ Produit mis à jour avec succès !";
            return RedirectToAction("Index");
        }

        // ❌ [GET] Page de confirmation de suppression
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product); // Views/Products/Delete.cshtml
        }

        // 🗑️ [POST] Suppression du produit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "🗑️ Produit supprimé avec succès !";

            return RedirectToAction("Index");
        }
    }
}
