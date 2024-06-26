using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModel.HorecaViewModel
{
    public class GetHorecaInformationsViewModel
    {
        public int Index { get; set; }
        public Guid Id { get; set; }

        public string? Governorate { get; set; }

        public string Name { get; set; } 

        public string? OwnerName { get; set; }

        public string? PurcasingManagerName { get; set; }

        public string? Type { get; set; }

        public string? Concept { get; set; }

        public string? OwnerPhone { get; set; }

        public string? PurchasingManagerPhone { get; set; }

        public string? Location { get; set; }

        public string? LocationDescription { get; set; }

        public string? MenuLink { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public List<string> HorecaImage {  get; set; }
        public List<GetHorecaStatictsInformationViewModel> getHorecaStatictsInformationViewModels { get; set; }

    }
}
