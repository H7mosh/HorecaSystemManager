using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.OrdersViewModel
{
    public class OrderToFilter
    {
        public List<OnlineOrderItemViewModelToFilter> onlineOrderItemsToFilter { get; set; }
        public List<StorageOrder> storageOrderList { get; set; }
    }

    public class OnlineOrderItemViewModelToFilter
    {
        public int OrderId { get; set; }
        public string Sku { get; set; }
        public double? Qtty { get; set; }
        public string Item { get; set; }
        public decimal? Price { get; set; }

    }

    public class OrderViewerViewModel
    {
        public Guid Id { get; set; }
        public int OrderId { get; set; }
        public int Secode { get; set; }
        public string PattrenNumber { get; set; }
        public string Batch { get; set; }
        public string ItemName { get; set; }
        public string Sku { get; set; }
        public double Quantity { get; set; }
        public string Price { get; set; }
        public string Cost { get; set; }
        public string Branch { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class StorageOrder
    {
        public string Name { get; set; }
        public int order { get; set; }
    }
}
