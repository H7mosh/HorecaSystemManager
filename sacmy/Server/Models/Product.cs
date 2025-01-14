using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public Guid BrandId { get; set; }

    public Guid? CollectionId { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid MaterialId { get; set; }

    public Guid? CatalogId { get; set; }

    public string? Sku { get; set; }

    public string? PatternNumber { get; set; }

    public string? Name { get; set; }

    public string? Decription { get; set; }

    public double Price { get; set; }

    public int? Points { get; set; }

    public int? Quantity { get; set; }

    public string? InnerType { get; set; }

    public int InnerTypeCount { get; set; }

    public string? InnerTypeImage { get; set; }

    public string? OuterType { get; set; }

    public int OuterTypeCount { get; set; }

    public string? OuterTypeImages { get; set; }

    public double? Height { get; set; }

    public double? Diameter { get; set; }

    public double? Top { get; set; }

    public double? Base { get; set; }

    public double? Volume { get; set; }

    public double? Weight { get; set; }

    public double? Area { get; set; }

    public string? Upc { get; set; }

    public string? Ean { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Advertise> Advertises { get; set; } = new List<Advertise>();

    public virtual Brand Brand { get; set; } = null!;

    public virtual Catalog? Catalog { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Collection? Collection { get; set; }

    public virtual ICollection<CustomerFavouriteProduct> CustomerFavouriteProducts { get; set; } = new List<CustomerFavouriteProduct>();

    public virtual ICollection<CustomerProductRelation> CustomerProductRelations { get; set; } = new List<CustomerProductRelation>();

    public virtual ICollection<CustomerViewedProduct> CustomerViewedProducts { get; set; } = new List<CustomerViewedProduct>();

    public virtual Material Material { get; set; } = null!;

    public virtual ICollection<OnlineOrderItem> OnlineOrderItems { get; set; } = new List<OnlineOrderItem>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
