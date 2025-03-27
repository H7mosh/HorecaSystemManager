using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.AdvertiseVewModels
{
    public class GetAdvertiseViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Sku {  get; set; }
        public string PatternCode { get; set; }
        public string Image { get; set; } = null!;
    }
}
