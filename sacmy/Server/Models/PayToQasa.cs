using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PayToQasa
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    public string? ReName { get; set; }

    public decimal? ReTotal { get; set; }

    public DateTime? Now { get; set; }

    public string? Note { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? EventId { get; set; }

    public string? Subb { get; set; }
}
