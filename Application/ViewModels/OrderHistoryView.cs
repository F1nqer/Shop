using System;

namespace Application.ViewModels
{
    public class OrderHistoryView
    {
        public int OrderId { get; set; }
        public string Address { get; set; }
        public int CardNumber { get; set; }
        public string State { get; set; }
        public DateTime ChangeAt { get; set; }
    }
}
