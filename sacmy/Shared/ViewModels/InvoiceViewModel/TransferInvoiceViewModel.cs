using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.InvoiceViewModel
{
    public class TransferInvoiceViewModel
    {
        public int SaleBillId { get; set; }
        public string Branch { get; set; }
        public string CarNo { get; set; }
        public string Company { get; set; } 
        public string customer
        {
            get
            {
                // You can define your logic here to derive the value based on the Branch
                // For example:
                switch (Branch.ToLower())
                {
                    case "zaxo":
                        return "pasabhace zaxo";
                    case "erbil":
                        return "pasabhace erbil";
                    
                    default:
                        return "pasabhace baghdad";
                }
            }
        }
        public string Address
        {
            get
            {
                // You can define your logic here to derive the value based on the Branch
                // For example:
                switch (Branch.ToLower())
                {
                    case "zaxo":
                        return "زاخو";
                    case "erbil":
                        return "أربيل";

                    default:
                        return "بغداد";
                }
            }
        }
    }

}
