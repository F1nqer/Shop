using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Orders { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int CardNumber { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
