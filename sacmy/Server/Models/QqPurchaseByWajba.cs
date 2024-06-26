using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QqPurchaseByWajba
{
    public string? Subb { get; set; }

    public int? Secode { get; set; }

    public string? Wajba { get; set; }

    public double? QttyPurch { get; set; }

    public double? AvrgPrice { get; set; }

    public double? Smtotal { get; set; }

    public DateOnly? Date { get; set; }
}
