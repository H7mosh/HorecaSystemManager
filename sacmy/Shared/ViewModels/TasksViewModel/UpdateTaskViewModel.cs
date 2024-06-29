using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TasksViewModel
{
    public class UpdateTaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid StatusId { get; set; }
        public Guid AssignedToEmployeeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
    }
}
