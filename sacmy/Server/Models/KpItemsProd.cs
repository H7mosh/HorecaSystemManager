using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class KpItemsProd
{
    public Guid Id { get; set; }

    public Guid MaterialId { get; set; }

    public Guid BrandId { get; set; }

    public string? BrandEn { get; set; }

    public string? BrandAr { get; set; }

    public string? BrandTr { get; set; }

    public string? BrandKr { get; set; }

    public string? BrandImage { get; set; }

    public Guid? CollectionId { get; set; }

    public string? CollectionEn { get; set; }

    public string? CollectionAr { get; set; }

    public string? CollectionTr { get; set; }

    public string? CollectionKr { get; set; }

    public string? CollectionImage { get; set; }

    public Guid? CatalogId { get; set; }

    public string? CatalogEn { get; set; }

    public string? CatalogAr { get; set; }

    public string? CatalogTr { get; set; }

    public string? CatalogKr { get; set; }

    public Guid? CategoryId { get; set; }

    public string? CategoryEn { get; set; }

    public string? CategoryAr { get; set; }

    public string? CategoryTr { get; set; }

    public string? CategoryKr { get; set; }

    public string? CategoryImage { get; set; }

    public string? Sku { get; set; }

    public string? PatternNumber { get; set; }

    public string? Name { get; set; }

    public string? Decription { get; set; }

    public double Price { get; set; }

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

    public string? StoreBranch { get; set; }

    public string? StoreName { get; set; }

    public string? ImageLink { get; set; }

    public int? Points { get; set; }

    public int? Quantity { get; set; }

    public DateTime CreatedDate { get; set; }
}
