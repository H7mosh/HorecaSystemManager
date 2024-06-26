using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TrackViewModel
{
    public class TrackViewModel
    {

        public int? CustomerId { get; set; }

        public int? InvoiceId { get; set; }

        public Guid? TypeId { get; set; }

        public string? Note { get; set; }

        public Guid? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

     
    }
}
