using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TrackViewModel
{
    public class AddCommentToExisingTrack
    {
        public Guid EmpId { get; set; }
        public Guid TrackId { get; set; }
        public Guid StateId { get; set; }
        public string Comment { get; set; }
        public Guid? AssignTo { get; set; }
        public DateTime? RescheduledAt{ get; set; }
        public bool isDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
