using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.AdvertiseVewModels
{
    public class CreateAdvertiseViewModel
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string Image { get; set; } = null!;
    }
}
