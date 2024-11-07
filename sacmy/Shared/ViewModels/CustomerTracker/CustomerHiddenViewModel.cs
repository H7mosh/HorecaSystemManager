using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.CustomerTracker
{
    public class CustomerHiddenViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public DateTime? LastDate { get; set; }
        public Guid? TaskId { get; set; }
        public string? TaskType { get; set; }
        public string? TaskStatus{ get; set; }
        public string? LastComment { get; set; }

    }
}
