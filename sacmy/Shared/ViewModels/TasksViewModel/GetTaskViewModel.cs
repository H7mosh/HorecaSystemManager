using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TasksViewModel
{
    public class GetTaskViewModel
    {
        public Guid Id {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid StatusId {  get; set; }
        public string AssignedToEmployee { get; set; }
        public Guid AssignedToEmployeeId { get; set; }
        public string EmployeeImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
