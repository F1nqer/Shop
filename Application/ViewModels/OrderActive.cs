using System.Collections.Generic;

namespace Application.ViewModels
{
    public class OrderActive
    {
        public int OrderId { get; set; }
        public string Address { get; set; }
        public int CardNumber { get; set; }
        public string State { get; set; }
        public List<ProductForOrder> Products { get; set; } = new List<ProductForOrder>();

    }
}
