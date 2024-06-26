using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class OrderInvoice
{
    public int Id { get; set; }

    public int? Idd { get; set; }

    public DateOnly? Date { get; set; }

    public string? Company { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public decimal? Tootal { get; set; }

    public string? Subb { get; set; }

    public string? EventId { get; set; }
}
