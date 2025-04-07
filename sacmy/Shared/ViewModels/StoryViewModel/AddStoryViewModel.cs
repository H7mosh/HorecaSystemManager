using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.StoryViewModel
{
    public class AddStoryViewModel
    {
        [Required]
        public Guid StoryId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
