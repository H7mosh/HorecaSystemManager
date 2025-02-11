using sacmy.Shared.ViewModels.InvoiceViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared
{
    public class InvoicePdfRequest
    {
        [Required]
        public BuyFatoraViewModel Invoice { get; set; }

        [Required]
        public List<InvoiceItemsViewModel> InvoiceItems { get; set; }
    }

}
