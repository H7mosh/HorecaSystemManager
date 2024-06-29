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

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public Guid AssignedToEmployee { get; set; }

        public Guid? StatusId { get; set; }

        public DateTime Deadline { get; set; }


    }
}
