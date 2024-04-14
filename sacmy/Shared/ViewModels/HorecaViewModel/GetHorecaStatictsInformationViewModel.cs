using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModel.HorecaViewModel
{
    public class GetHorecaStatictsInformationViewModel
    {

        public int? Rating { get; set; }

        public int? TablesCount { get; set; }

        public int? ChairsCount { get; set; }

        public int? BranchesCount { get; set; }

        public bool? IsHeSafeenAhmedCompanyCustomer { get; set; }

        public bool? IsHePasabahceBuyer { get; set; }

        public string? PasabahceReason { get; set; }

        public int? PasabahcePercentage { get; set; }

        public bool? IsHeBonnaBuyer { get; set; }

        public string? BonnaReason { get; set; }

        public int? BonnaPercentage { get; set; }

        public bool? IsHeNudeBuyer { get; set; }

        public string? NudeReason { get; set; }

        public int? NudePercentage { get; set; }

        public bool? IsHeBuyingFromAnotherGlassAgency { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid HorecaId {  get; set; }

    }
}
