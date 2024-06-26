using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ExchangeFatora
{
    public int IdExcha { get; set; }

    public int IdSales { get; set; }

    public int IdPurch { get; set; }

    public DateOnly? Date { get; set; }

    public string? Customer { get; set; }

    public string? Company { get; set; }

    public string? Notes { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? Subb { get; set; }

    public string? SalEventId { get; set; }

    public DateOnly? Datee { get; set; }

    public bool? Checkeed { get; set; }

    public bool? Trans { get; set; }

    public string? FromStor { get; set; }

    public string? TooStor { get; set; }

    public string? PuEventId { get; set; }
}
