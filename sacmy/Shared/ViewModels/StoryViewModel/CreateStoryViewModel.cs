using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.StoryViewModel
{
    public class CreateStoryViewModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string MediaUrl { get; set; }

        [Required]
        public string MediaType { get; set; }

        public string Description { get; set; } = "product";

        public string Message { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }
}
