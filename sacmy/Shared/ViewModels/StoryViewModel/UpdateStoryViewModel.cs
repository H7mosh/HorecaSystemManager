using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.StoryViewModel
{
    public class UpdateStoryViewModel
    {
        public string MediaUrl { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
