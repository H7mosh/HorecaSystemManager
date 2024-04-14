using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class NnRaseedItem
{
    public int Secode { get; set; }

    public string? Cod { get; set; }

    public string? CodIqd { get; set; }

    public string? Item { get; set; }

    public string? Type { get; set; }

    public string? Factory { get; set; }

    public double? PurchQtty { get; set; }

    public double? SalesQtty { get; set; }

    public double? ReturnQtty { get; set; }

    public double? RemainQtty { get; set; }
}
