using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.LowStockViewModels
{
    public class NotificationItem
    {
        public Guid ID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public Guid EmployeeID { get; set; }
        public decimal Quantity { get; set; }
        public string FirebaseToken { get; set; }
        public int Threshold { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsDeleted { get; set; }
    }
}
