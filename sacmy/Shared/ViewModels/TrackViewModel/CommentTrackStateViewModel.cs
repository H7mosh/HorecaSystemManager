using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TrackViewModel
{
    public class CommentTrackStateViewModel
    {
        public Guid Id { get; set; }

        public string? StateEn { get; set; }

        public string? StateAr { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
