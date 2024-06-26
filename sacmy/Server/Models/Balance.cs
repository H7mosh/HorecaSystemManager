using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Balance
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public string? Btype { get; set; }

    public decimal? Btotal { get; set; }

    public DateTime? Now { get; set; }

    public string? Note { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? Subb { get; set; }
}
