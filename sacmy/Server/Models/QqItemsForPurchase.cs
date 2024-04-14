using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QqItemsForPurchase
{
    public string? Barcod { get; set; }

    public string? Cod { get; set; }

    public string? CodIqd { get; set; }

    public string? Item { get; set; }

    public string? BoxContain { get; set; }

    public string? Type { get; set; }

    public string? Factory { get; set; }

    public string? QiyasUnit { get; set; }

    public int Secode { get; set; }

    public double? Count { get; set; }

    public double? Mkaab { get; set; }
}
