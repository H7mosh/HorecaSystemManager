using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CountAllCostomerSafi
{
    public string? Customer { get; set; }

    public string? Address { get; set; }

    public string? Mob { get; set; }

    public string? Subb { get; set; }

    public decimal? BuyRemain { get; set; }

    public decimal? Pttotal { get; set; }

    public decimal? Pftotal { get; set; }

    public decimal? Safiremain { get; set; }

    public decimal? RetRemain { get; set; }

    public string? CostType { get; set; }
}
