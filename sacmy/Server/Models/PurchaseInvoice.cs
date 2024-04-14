using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PurchaseInvoice
{
    public int Id { get; set; }

    public int? Idd { get; set; }

    public DateTime? Date { get; set; }

    public string? Company { get; set; }

    public string? Address { get; set; }

    public string? Notes { get; set; }

    public decimal? Dolar { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public decimal? Tootal { get; set; }

    public string? Driver { get; set; }

    public string? CarNo { get; set; }

    public string? DriverMob { get; set; }

    public string? Subb { get; set; }

    public string? Wajba { get; set; }

    public string? EventId { get; set; }
}
