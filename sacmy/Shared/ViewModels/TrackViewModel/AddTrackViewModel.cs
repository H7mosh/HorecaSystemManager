using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TrackViewModel
{
    public class AddTrackViewModel
    {
        public int? CustomerId { get; set; }

        public int? InvoiceId { get; set; }

        public Guid EmplyeeId { get; set; }

        public Guid? TypeId { get; set; }

        public string? Note { get; set; }

        public Guid StateId { get; set; }

        public string Comment { get; set; }

        public Guid? AssignTo { get; set; }

        public DateTime? ReOpenAt { get; set; }

    }

         
}
