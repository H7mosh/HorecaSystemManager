using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CompanyMassarif
{
    public int? Id { get; set; }

    public decimal? Total { get; set; }

    public string? Masraftype { get; set; }

    public string? Note { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public DateTime? Now { get; set; }

    public string? Subb { get; set; }

    public DateOnly? Date { get; set; }

    public string? Symbol { get; set; }
}
