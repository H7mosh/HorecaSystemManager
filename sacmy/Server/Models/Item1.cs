using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Item1
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public Guid? SecondryCategoryId { get; set; }

    public Guid? SpecificationId { get; set; }

    public Guid? Images { get; set; }

    public string Sku { get; set; } = null!;

    public string? SeCode { get; set; }

    public string Series { get; set; } = null!;

    public string PattrenNo { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Decription { get; set; }

    public int? Points { get; set; }

    public double Price { get; set; }

    public int? MinimumQuantityWarning { get; set; }

    public int Quantity { get; set; }

    public string? InnerType { get; set; }

    public byte InnerTypeCount { get; set; }

    public string InnerTypeImage { get; set; } = null!;

    public string? OuterType { get; set; }

    public byte OuterTypeCount { get; set; }

    public string? OuterTypeImage { get; set; }

    public double? Height { get; set; }

    public double? Diameter { get; set; }

    public double? Top { get; set; }

    public string? Base { get; set; }

    public double? Volume { get; set; }

    public double? Weight { get; set; }

    public double? Area { get; set; }

    public string? Function { get; set; }

    public string? ImageName { get; set; }

    public string? Catalog { get; set; }

    public string? Upc { get; set; }

    public string? Ean { get; set; }

    public string? GlassType { get; set; }

    public string? ProductionTechnique { get; set; }

    public string? Color { get; set; }

    public string Category { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual ICollection<FeatureItem> FeatureItems { get; set; } = new List<FeatureItem>();

    public virtual ICollection<ItemImage> ItemImages { get; set; } = new List<ItemImage>();

    public virtual ICollection<ItemsItemSpecification> ItemsItemSpecifications { get; set; } = new List<ItemsItemSpecification>();

    public virtual ICollection<OnlineOrderItem> OnlineOrderItems { get; set; } = new List<OnlineOrderItem>();

    public virtual SecondryCategory? SecondryCategory { get; set; }
}
