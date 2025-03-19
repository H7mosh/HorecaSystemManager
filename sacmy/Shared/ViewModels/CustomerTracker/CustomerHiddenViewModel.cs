using sacmy.Shared.ViewModels.StickNoteViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerTracker
{
    public class CustomerHiddenViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DateTime LastDate { get; set; }
        public int DaysSinceLastInvoice { get; set; } 
        public Guid? TaskId { get; set; }
        public string TaskStatus { get; set; }
        public string LastComment { get; set; }
        public List<GetStickyNoteViewModel> StickyNotes { get; set; }
    }
}
