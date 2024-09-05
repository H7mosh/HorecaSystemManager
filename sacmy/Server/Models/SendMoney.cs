using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class SendMoney
{
    public int Id { get; set; }

    public string? Company { get; set; }

    public string? Address { get; set; }

    public decimal? Total { get; set; }

    public decimal? Amola { get; set; }

    public decimal? SuTotal { get; set; }

    public string? Offiice { get; set; }

    public string? Notes { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string DecreaseFrom { get; set; } = null!;

    public decimal? Dolar { get; set; }

    public decimal? Iqd { get; set; }

    public bool? Run { get; set; }

    public string? Subb { get; set; }

    public string? EventId { get; set; }

    public DateTime? Date { get; set; }

    public string? Symbol { get; set; }
}
