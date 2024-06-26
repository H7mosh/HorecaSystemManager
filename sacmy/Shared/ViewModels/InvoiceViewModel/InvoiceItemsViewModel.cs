using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.InvoiceViewModel
{
    public class InvoiceItemsViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string PatternCode { get; set; }
        public string Name { get; set; }
        public string? Factory { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Cost { get; set; }
        public string? Batch { get; set; }
    }
}
