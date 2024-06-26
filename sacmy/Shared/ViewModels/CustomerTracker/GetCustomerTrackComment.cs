using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerTracker
{
    public class GetCustomerTrackComment
    {
        public string Type { get; set; }
        public string Comment { get; set; }
        public string State {  get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeImage { get; set; }
        public string EmployeeJobTitle { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
