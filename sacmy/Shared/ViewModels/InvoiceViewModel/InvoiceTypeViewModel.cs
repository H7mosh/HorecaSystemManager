using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.InvoiceViewModel
{
    public class InvoiceTypeViewModel
    {
        public string Type { get; set; }
        public int InvoiceTypeCount {  get; set; }
        public double TotalAmount { get; set; }
        public double Percentage {  get; set; }
    }
}
