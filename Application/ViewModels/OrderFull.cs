using System.Collections.Generic;

namespace Application.ViewModels
{
    public class OrderFull
    {
        public int OrderId { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public int CardNumber { get; set; }
        public List<ProductForOrder> Products { get; set; } = new List<ProductForOrder>();
        public List<ProductHistory> ProductsHistory { get; set; } = new List<ProductHistory>();
        public List<OrderHistoryView> OrdersHistory { get; set; } = new List<OrderHistoryView>();

    }
}