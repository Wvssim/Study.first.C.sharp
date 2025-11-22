using Microsoft.EntityFrameworkCore;
using MyMarket.Data;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Ajouter le contexte de base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ Ajouter la mémoire distribuée (obligatoire pour les sessions)
builder.Services.AddDistributedMemoryCache();

// 3️⃣ Ajouter la gestion de session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de validité
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4️⃣ Ajouter l’accès au HttpContext (✅ indispensable pour afficher les boutons Login/Register)
builder.Services.AddHttpContextAccessor();

// 5️⃣ Ajouter les contrôleurs avec vues
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 6️⃣ Activer la session AVANT l’autorisation
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
