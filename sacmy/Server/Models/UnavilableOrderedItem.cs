using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class UnavilableOrderedItem
{
    public Guid Id { get; set; }

    public string Sku { get; set; } = null!;

    public string PattrenCode { get; set; } = null!;

    public int Quantity { get; set; }

    public int? Secode { get; set; }

    public Guid ItemId { get; set; }

    public int BillId { get; set; }
}
