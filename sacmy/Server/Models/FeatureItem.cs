using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class FeatureItem
{
    public Guid Id { get; set; }

    public bool IsNew { get; set; }

    public bool IsBestSeller { get; set; }

    public Guid ItemId { get; set; }

    public DateTime? Date { get; set; }

    public string? SkuCod { get; set; }

    public virtual Item1 Item { get; set; } = null!;
}
