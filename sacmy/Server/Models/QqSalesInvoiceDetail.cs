using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class QqSalesInvoiceDetail
{
    public int Id { get; set; }

    public string? Subb { get; set; }

    public double? Quantity { get; set; }

    public int? Secode { get; set; }
}
