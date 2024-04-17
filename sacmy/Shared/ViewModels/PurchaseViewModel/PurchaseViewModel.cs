using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.PurchaseViewModel
{
    public class PurchaseViewModel
    {
        public string Code { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string CartonCost { get; set; }
    }
}
