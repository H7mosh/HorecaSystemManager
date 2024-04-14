using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ZzOnlineOrderTotal
{
    public int OrderId { get; set; }

    public decimal? Total { get; set; }

    public string? Sub { get; set; }
}
