using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int CardNumber { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();

    }
}
