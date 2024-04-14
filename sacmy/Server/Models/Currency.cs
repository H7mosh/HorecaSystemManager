using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Currency
{
    public string Id { get; set; } = null!;

    public decimal ExchangeRate { get; set; }

    public DateTime CreatedDate { get; set; }
}
