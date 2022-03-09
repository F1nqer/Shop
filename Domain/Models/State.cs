using System.Collections.Generic;

namespace Domain.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<OrderHistory> OrdersHistory { get; set; } = new List<OrderHistory>();
    }
}
