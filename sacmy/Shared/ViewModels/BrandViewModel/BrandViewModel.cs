using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.BrandViewModel
{
    public class BrandViewModel
    {
        public Guid Id { get; set; }

        public string NameEn { get; set; } = null!;

        public string? NameAr { get; set; }

        public string? NameTr { get; set; }

        public string? NameKr { get; set; }

        public string? Image { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
