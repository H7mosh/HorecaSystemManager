using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Batch
{
    public Guid Id { get; set; }

    public string? Branch { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? PurchaseInvoiceId { get; set; }

    public virtual PurchaseInvoice? PurchaseInvoice { get; set; }
}
