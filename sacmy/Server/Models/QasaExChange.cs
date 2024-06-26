using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QasaExChange
{
    public int Id { get; set; }

    public DateOnly? Datee { get; set; }

    public string? Note { get; set; }

    public bool? Fromm { get; set; }

    public string? Symbol { get; set; }

    public decimal? Tootall { get; set; }

    public bool? Too { get; set; }

    public string? SymbolL { get; set; }

    public decimal? TotaLlafter { get; set; }

    public decimal? CurrencyPrice { get; set; }

    public string? Treasurer { get; set; }

    public string? Qasa { get; set; }

    public string? Uuser { get; set; }

    public DateTime? Now { get; set; }

    public string? Subb { get; set; }
}
