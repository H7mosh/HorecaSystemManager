using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public Guid? SeriesOrCollectionId { get; set; }

    public string Sku { get; set; } = null!;

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public int? Points { get; set; }

    public int? Quantity { get; set; }

    public int? Box { get; set; }

    public string? BoxType { get; set; }

    public int? Piece { get; set; }

    public string? PieceType { get; set; }

    public string? Ean { get; set; }

    public string? Upc { get; set; }

    public double? Height { get; set; }

    public string? HeightUnit { get; set; }

    public double? Weight { get; set; }

    public string? WeightUnit { get; set; }

    public double? Length { get; set; }

    public string? LengthUnit { get; set; }

    public double? Top { get; set; }

    public string? TopUnit { get; set; }

    public double? Base { get; set; }

    public string? BaseUnit { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual SeriesOrCollectionOrCategory? SeriesOrCollection { get; set; }
}
