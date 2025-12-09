using System.ComponentModel.DataAnnotations.Schema;

namespace MyMarket.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Relation vers Order
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        // Relation vers Product
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal Total => UnitPrice * Quantity;
    }
}
