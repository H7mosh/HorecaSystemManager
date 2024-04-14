using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Bonna
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public double? Price { get; set; }

    public int? Quantity { get; set; }

    public double? Weight { get; set; }

    public double? Volume { get; set; }

    public string? Barcode { get; set; }

    public int? Pcs { get; set; }

    public string? Collection { get; set; }

    public double? Length { get; set; }

    public double? Height { get; set; }

    public double? Width { get; set; }

    public string? MaterialType { get; set; }

    public string? KitchenType { get; set; }

    public string? ProductType { get; set; }

    public int? Box { get; set; }

    public string? Glaze { get; set; }

    public string? Decor { get; set; }

    public string? EdgeChipWarranty { get; set; }
}
