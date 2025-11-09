# 🛍️ MyMarket

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-6.0-blue?logo=dotnet)](https://dotnet.microsoft.com/) 
[![C#](https://img.shields.io/badge/C%23-9.0-blue?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/) 
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?logo=bootstrap)](https://getbootstrap.com/) 
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

---

## 📖 Project Overview

**MyMarket** is a functional e-commerce web application prototype built with **ASP.NET Core MVC**.  

The project currently implements:

- Basic **login** system (any credentials accepted for now)  
- **Admin dashboard** showing all products and allowing addition of new products  
- **Product catalog** on the homepage  
- Individual **product details** pages  
- **Shopping cart** with add/remove functionality stored in **local storage**  

The project is designed to evolve into a complete e-commerce platform with proper user roles, database integration, and payment processing.

---

## 🛠️ Current Features

- **Login page**: basic login for testing  
- **Dashboard**: displays products with Id, Name, Description, Price, Stock  
- **Add Products**: form to add products dynamically  
- **Product listing**: products displayed on homepage  
- **Product details page**: view individual product info  
- **Shopping Cart**: add/remove products, cart persists in local storage  

---

## 🔮 Future Improvements

- Implement **user authentication and authorization** with roles (admin, customer)  
- Integrate a **database** (e.g., SQL Server) to persist users, products, and orders  
- Add **secure password management** (hashing, validation)  
- Implement **payment gateway integration** (Stripe, PayPal)  
- Improve **dashboard**: editing/deleting products, viewing orders  
- Add **search, filters, and categories** in product catalog  
- Responsive **UI improvements** and error handling  

---

## 💻 Technologies

| Technology | Usage |
|------------|-------|
| ![C#](https://img.shields.io/badge/C%23-9.0-blue?logo=c-sharp) | Core language |
| ![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-6.0-blue?logo=dotnet) | Backend MVC framework |
| ![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?logo=bootstrap) | Responsive UI |
| HTML / CSS / Razor | Page templates and layouts |
| Local Storage | Client-side cart persistence |

---

## 🚀 Installation & Launch

1. Clone the repository:

```bash
git clone git@github.com:Wvssim/Study.first.C.sharp.git
cd MyMarket
