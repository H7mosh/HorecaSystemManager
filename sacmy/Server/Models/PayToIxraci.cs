using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PayToIxraci
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public string? IxraciN { get; set; }

    public string? Address { get; set; }

    public decimal? Total { get; set; }

    public string? Typee { get; set; }

    public string? Notes { get; set; }

    public DateTime? Now { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? Subb { get; set; }

    public string? EventId { get; set; }

    public bool? Runn { get; set; }

    public string? FrmOffice { get; set; }

    public string? Symbol { get; set; }
}
