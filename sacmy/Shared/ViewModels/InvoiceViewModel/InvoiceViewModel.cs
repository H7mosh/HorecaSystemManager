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
        public string CustomerName {  get; set; }
        public string CustomerType { get; set; }
        public string Address { get; set; }
        public string InvoiceBranch { get; set; }
        public double Total { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? TrackId { get; set; }
        public string? TrackType { get; set; }
        public string? State { get; set; }
    }
}
