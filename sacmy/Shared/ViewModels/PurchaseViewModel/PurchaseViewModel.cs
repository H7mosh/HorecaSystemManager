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
        public string Description { get; set; }
        public string Weight { get; set; }
        public double Quantity { get; set; }
        public double CartoonPrice { get; set; }
        public string PiecePrice { get; set; }
        public double TotalPrice { get; set; }
        public string Storage {  get; set; }
        public string Sector {  get; set; }
        public int Row {  get; set; }
        public int Column { get; set; }
    }
}
