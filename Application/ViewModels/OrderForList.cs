using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class OrderForList
    {
        public int OrderId { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
    }
}
