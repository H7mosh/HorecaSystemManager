using System;
using System.Collections.Generic;
using System.Globalization;
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
        public double PriceValue => ParseNumberSafely(Price);
        public double CostValue => ParseNumberSafely(Cost);

        public string Branch { get; set; }
        public bool IsAvailable { get; set; }

        private static double ParseNumberSafely(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                return result;

            if (double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                return result;

            return 0;
        }
    }

    public class StorageOrder
    {
        public string Name { get; set; }
        public int order { get; set; }
    }
}
