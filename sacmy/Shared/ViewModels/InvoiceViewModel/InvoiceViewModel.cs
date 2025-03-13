using sacmy.Shared.ViewModels.CustomerTracker;
using sacmy.Shared.ViewModels.StickNoteViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.InvoiceViewModel
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerType { get; set; }
        public string Address { get; set; }
        public string InvoiceBranch { get; set; }
        public double Total { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPdfGenerated { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? TaskId { get; set; }
        public string? TaskType { get; set; }
        public string? TaskStatus { get; set; }
        public string? LastComment { get; set; }
        public List<GetStickyNoteViewModel> StickyNotes { get; set; } = new List<GetStickyNoteViewModel>();
    }
}
