using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ProductForOrder
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal PriceSum { get; set; }
        public int Count { get; set; }
    }
}
