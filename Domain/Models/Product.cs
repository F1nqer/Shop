using System.Collections.Generic;

namespace Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
        public List<OrderHistory> OrdersHistory { get; set; } = new List<OrderHistory>();
        public List<OrderProductsHistory> OrderProductsHistory { get; set; } = new List<OrderProductsHistory>();

    }
}
