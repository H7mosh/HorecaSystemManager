using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.InvoiceViewModel
{
    public class BuyFatoraViewModel
    {
        public int Id { get; set; }
        public string Customer {  get; set; }
        public string CustomerId { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerFirebaseToken { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Payed { get; set; }
        public decimal? Remaing { get; set; }
        public decimal? Tootal { get; set; }
        public decimal? TotalWeight { get; set; }
        public decimal? TotalArea { get; set; }
        public string branch { get; set; }
        public bool IsItTheMainOne { get; set; }
        public bool IsPdfGenerated { get; set; }
    }
}
