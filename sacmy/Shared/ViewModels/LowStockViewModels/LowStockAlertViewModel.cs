using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.LowStockViewModels
{
    public class LowStockAlertViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsProcessed { get; set; }
        public List<LowStockEmployeeViewModel> Recipients { get; set; }
    }

}
