using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.InvoiceViewModel
{
    public class InvoiceItemsPdfRequest
    {
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime GenerationDate { get; set; }
        public List<InvoiceItemPdfModel> Items { get; set; }
    }

    public class InvoiceItemPdfModel
    {
        public string PatternCode { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Factory { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal Cost { get; set; }
        public string Batch { get; set; }
        public bool IsChecked { get; set; }
        public string Notes { get; set; }
    }
}
