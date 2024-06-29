using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TasksViewModel
{
    public class GetTaskNotes
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public string? FileLink { get; set; }
        public Guid CreatedBy { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeImage { get; set; }
        public string EmpolyeeRole {  get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
