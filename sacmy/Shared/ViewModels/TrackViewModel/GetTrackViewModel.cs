using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TrackViewModel
{
    public class GetTrackViewModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string CustomerName { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public Guid? AssignedToId { get; set; }
        public string AssignedToName { get; set; }
        public string State { get; set; }
        public DateTime? ReschedueldAt { get; set; }
        public string Category { get; set; } // فئة التتبع
    }

}
