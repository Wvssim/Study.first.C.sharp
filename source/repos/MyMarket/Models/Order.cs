using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyMarket.Models
{
    public class Order
    {
        public int Id { get; set; }

        // si tu lies un utilisateur :
        public int? UserId { get; set; }    // facultatif si pas d'authentification persistante
        public string? UserEmail { get; set; }
        public string? UserFullName { get; set; }
        public string? UserPhone { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Paid, Shipped, Cancelled

        public List<OrderItem> Items { get; set; } = new();
    }
}
