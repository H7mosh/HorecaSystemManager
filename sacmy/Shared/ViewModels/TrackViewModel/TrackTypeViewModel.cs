using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TrackViewModel
{
    public class TrackTypeViewModel
    {
        public Guid Id { get; set; }

        public string? TypeAr { get; set; }

        public string? TypeEn { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
