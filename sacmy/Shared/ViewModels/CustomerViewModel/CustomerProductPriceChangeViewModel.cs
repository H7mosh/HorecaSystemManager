using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerViewModel
{
    public class CustomerProductPriceChangeViewModel
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public string ProductPatternNumber { get; set; }
        public bool IsDiscounted { get; set; }
        public bool IsRaised { get; set; }
        public string PriceChange { get; set; }
        public double FinalPrice { get; set; }
    }
}
