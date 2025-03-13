using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.LowStockViewModels
{
    public class MonitorProductViewModel
    {
        public Guid ProductID { get; set; }
        public Guid EmployeeID { get; set; }
        public int Threshold { get; set; }
    }
}
