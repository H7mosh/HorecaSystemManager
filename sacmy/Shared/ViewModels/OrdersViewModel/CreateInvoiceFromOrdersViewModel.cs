using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.OrdersViewModel
{
    public class InvoiceFromOrderViewModel
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string Sub { get; set; }
        public double Payed { get; set; }
        public double Remain { get; set; }
        public double Total { get; set; }
        public double Hamalya { get; set; }
        public DateTime FatoraDate { get; set; }
        public string Note { get; set; }
        public bool Checked { get; set; }
        public Collection<InvoiceFromOrderItemsViewModel> invoiceFromOrderItemsViewModel { get; set; }
    }

    public class InvoiceFromOrderItemsViewModel
    {
        public Guid ItemId { get; set; }
        public int Secode { get; set; }
        public string Wajba { get; set; }
        public string Sku { get; set; }
        public int OrderItemId { get; set; }
        public double Quantity { get; set; }
        public string Storage { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public int Point { get; set; }
    }
}
