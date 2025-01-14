using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.OrdersViewModel
{
    public class OrderItemViewModel
    {
        public Guid ItemId { get; set; }
        public string Sku { get; set; }
        public string PatternCode { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int Point { get; set; }
    }
}
