# 🛍️ MyMarket — Version 2

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-blue?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Entity Framework Core](https://img.shields.io/badge/Entity_Framework_Core-8.0-green?logo=dotnet)](https://learn.microsoft.com/en-us/ef/core/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple?logo=bootstrap)](https://getbootstrap.com/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-Express-red?logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

---

## 📖 Project Overview

**MyMarket version 2** is an upgraded version of the MyMarket e-commerce web application built with **ASP.NET Core MVC** and **Entity Framework Core**.  
Unlike version 1, which used local storage for product data, this new release introduces **a real SQL Server database**, **full CRUD operations**, and a **secure admin dashboard**.

This version brings MyMarket closer to a production-ready e-commerce solution by adding true data persistence, authentication, and a better admin experience.

---

## 🧩 Key Improvements from v1

| Feature | Version 1 | Version 2 |
|----------|------------|-----------|
| Data Storage | Local (in-memory / lists) | Persistent via SQL Server (EF Core) |
| Product CRUD | Add only | Add / Edit / Delete |
| Authentication | Basic mock login | Session-based Admin authentication |
| Dashboard | Basic list | Full admin dashboard with styled UI |
| Database | ❌ None | ✅ SQL Server + EF Core migrations |
| UI | Basic Bootstrap | Enhanced Bootstrap 5 layout and design |

---

## 🛠️ Core Features (V2)

- 🔑 **Admin Login** — simple session-based authentication system  
- 📊 **Admin Dashboard** — add, edit, delete, and view all products  
- 🧱 **Entity Framework Core Integration** — database-first ORM layer  
- 🧩 **Migrations** — full migration history managed via EF tools  
- 🛒 **Frontend Product Catalog** — dynamic product listing from database  
- 💾 **SQL Server Persistence** — data remains after restarting the app  
- ⚙️ **Session Management** — maintains login state for admin  

---

### 🧠 Planned Features

- 🛍️ **User Accounts & Orders** — full customer authentication and order management  
- 💳 **Online Payment Integration** — support for Stripe, PayPal, and CMI  
- 🧾 **Invoice Generation (PDF)** — downloadable invoices for admin and customers  
- 🔎 **Advanced Search & Filters** — product search by category, name, and price  
- 🌐 **Multilingual Support** — English, French, and Arabic interfaces  
- 📦 **Category Management** — better organization of product catalog  
- 🖼️ **Product Image Upload** — cloud-based or local image storage  
- 🧾 **Activity Logs** — track admin and user actions for better management  

---

## ⚙️ Installation & Setup

### 1️⃣ Clone the repository
```bash
git clone git@github.com:Wvssim/Study.first.C.sharp.git
cd MyMarket
