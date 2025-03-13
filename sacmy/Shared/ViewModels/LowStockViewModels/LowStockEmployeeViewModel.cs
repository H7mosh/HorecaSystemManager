using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.LowStockViewModels
{
    public class LowStockEmployeeViewModel
    {
        public Guid EmployeeID { get; set; }
        public string Name { get; set; }
        public string FirebaseToken { get; set; }
        public int Threshold { get; set; }
    }
}
