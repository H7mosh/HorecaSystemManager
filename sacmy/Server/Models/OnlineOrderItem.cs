using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class OnlineOrderItem
{
    public int OrR { get; set; }

    public int OrderId { get; set; }

    public Guid ItemId { get; set; }

    public Guid? SecondryCategoryId { get; set; }

    public Guid? CategoryId { get; set; }

    public string? Sku { get; set; }

    public string? MoldNo { get; set; }

    public string? Barcod { get; set; }

    public string? Item { get; set; }

    public double? Qtty { get; set; }

    public decimal? Price { get; set; }

    public int? Point { get; set; }

    public decimal? Total { get; set; }

    public string? Secod { get; set; }

    public string? Sub { get; set; }

    public bool? IsCancelled { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? CancelledDate { get; set; }

    public virtual Category1? Category { get; set; }

    public virtual Product Item1 { get; set; } = null!;

    public virtual Item1 ItemNavigation { get; set; } = null!;

    public virtual OnlineOrder Order { get; set; } = null!;

    public virtual SecondryCategory? SecondryCategory { get; set; }
}
