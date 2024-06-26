using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QqqOrderItemManager
{
    public int BuId { get; set; }

    public int Id { get; set; }

    public DateOnly? Datee { get; set; }

    public string? Customer { get; set; }

    public string? Address { get; set; }

    public string? CodIqd { get; set; }

    public string? Codd { get; set; }

    public string? Itemm { get; set; }

    public double? Quantity { get; set; }

    public string? Wajba { get; set; }

    public decimal? Prise { get; set; }

    public decimal? Total { get; set; }

    public string? Uuser { get; set; }

    public string? Subb { get; set; }

    public bool? Checkeed { get; set; }
}
