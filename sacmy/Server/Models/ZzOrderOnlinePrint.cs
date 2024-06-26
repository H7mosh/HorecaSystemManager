using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ZzOrderOnlinePrint
{
    public int OrderId { get; set; }

    public DateOnly? Date { get; set; }

    public string? CostumerName { get; set; }

    public string? Sub { get; set; }

    public string? Note { get; set; }

    public bool? Checked { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? Address { get; set; }

    public int OrR { get; set; }

    public string? Sku { get; set; }

    public string? MoldNo { get; set; }

    public string? Barcod { get; set; }

    public string? Item { get; set; }

    public double? Qtty { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public string? Secod { get; set; }

    public decimal? SumTotal { get; set; }
}
