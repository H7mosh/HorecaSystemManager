using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QqqPurchaseInvoiceSumm
{
    public int Id { get; set; }

    public string? Company { get; set; }

    public string? Subb { get; set; }

    public decimal? SmTotal { get; set; }
}
