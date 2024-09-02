using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.TasksViewModel
{
    public class PostTaskViewModel
    {
        public Guid Id { get; set; }

        public Guid TaskTypeId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int? CustomerId { get; set; }
        public int? InvoiceId { get; set; }

        public Guid AssignedToEmployee { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? StatusId { get; set; }

        public DateTime Deadline { get; set; }


    }
}
