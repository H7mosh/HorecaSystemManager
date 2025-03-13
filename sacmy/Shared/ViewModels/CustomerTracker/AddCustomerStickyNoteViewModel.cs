using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerTracker
{
    // CustomerStickyNoteViewModel.cs
    public class AddCustomerStickyNoteViewModel
    {
        public string TableName { get; set; } = "Customers"; 
        public string RecordId { get; set; } 
        public Guid EmployeeId { get; set; }
        public string Note { get; set; }
    }
}
