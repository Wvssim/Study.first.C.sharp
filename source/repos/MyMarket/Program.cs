using Microsoft.EntityFrameworkCore;
using MyMarket.Data;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Ajouter le contexte de base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ Ajouter la gestion de session
builder.Services.AddSession();

// 3️⃣ Ajouter les contrôleurs avec vues
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

// 4️⃣ Activer la session AVANT l’autorisation
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
