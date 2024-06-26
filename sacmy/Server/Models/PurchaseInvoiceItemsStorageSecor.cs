using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class PurchaseInvoiceItemsStorageSecor
{
    public Guid Id { get; set; }

    public Guid? BatchId { get; set; }

    public Guid? StorageSectorId { get; set; }

    public int? PurchaseInvoiceItemId { get; set; }

    public virtual PurchaseInvoiceItem? PurchaseInvoiceItem { get; set; }

    public virtual StorageSector? StorageSector { get; set; }
}
