using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class HorecaInformation
{
    public Guid Id { get; set; }

    public string? Governorate { get; set; }

    public string Name { get; set; } = null!;

    public string? OwnerName { get; set; }

    public string? PurcasingManagerName { get; set; }

    public string? Type { get; set; }

    public string? Concept { get; set; }

    public string? OwnerPhone { get; set; }

    public string? PurchasingManagerPhone { get; set; }

    public string? Location { get; set; }

    public string? LocationDescription { get; set; }

    public string? Note { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<BonnaCollection> BonnaCollections { get; set; } = new List<BonnaCollection>();

    public virtual Employee? CreatedByNavigation { get; set; }

    public virtual ICollection<HorecaImage> HorecaImages { get; set; } = new List<HorecaImage>();

    public virtual ICollection<HorecaMenuImage> HorecaMenuImages { get; set; } = new List<HorecaMenuImage>();

    public virtual ICollection<HorecaStatictsInformation> HorecaStatictsInformations { get; set; } = new List<HorecaStatictsInformation>();

    public virtual ICollection<OtherBrand> OtherBrands { get; set; } = new List<OtherBrand>();

    public virtual ICollection<OtherGlassAgency> OtherGlassAgencies { get; set; } = new List<OtherGlassAgency>();

    public virtual ICollection<PasabahceSeries> PasabahceSeries { get; set; } = new List<PasabahceSeries>();
}
