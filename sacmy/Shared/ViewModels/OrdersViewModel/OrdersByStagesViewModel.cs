using sacmy.Shared.ViewModels.InvoiceViewModel;
using sacmy.Shared.ViewModels.StickNoteViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.OrdersViewModel
{
    public class OrdersByStagesViewModel
    {
        public Guid OrderStageId { get; set; }
        public string OrderStageNameEn { get; set; }
        public string OrderStageNameAr { get; set; }
        public string OrderStageNameTr { get; set; }
        public string OrderStageNameKr { get; set; }
        public int Sequence { get; set; }
        public int OrderCount { get; set; }
        public List<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string CostumerName { get; set; }
        public string CostumerFirebaseToken { get; set; }
        public string Address { get; set; }
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        public List<BuyFatoraViewModel> Invoices { get; set; } = new List<BuyFatoraViewModel>();
        public List<GetStickyNoteViewModel>? StickyNotes { get; set; }
    }
}
