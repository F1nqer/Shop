using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
