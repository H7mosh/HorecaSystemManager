using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PayFromQasa
{
    public int Id { get; set; }

    public string? ReName { get; set; }

    public decimal? Toetal { get; set; }

    public DateTime? Now { get; set; }

    public string? Note { get; set; }

    public string? Treasurer { get; set; }

    public string? Uuser { get; set; }

    public string? EventId { get; set; }

    public string? Subb { get; set; }

    public DateOnly? Date { get; set; }
}
