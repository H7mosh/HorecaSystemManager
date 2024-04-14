using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class CurrencyType
{
    public int Id { get; set; }

    public string? CurrencySymbol { get; set; }

    public string? CurrencyName { get; set; }
}
