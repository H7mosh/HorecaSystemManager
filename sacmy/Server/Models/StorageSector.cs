using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class StorageSector
{
    public Guid Id { get; set; }

    public Guid? StorageId { get; set; }

    public Guid? SectorId { get; set; }

    public int? RowIndex { get; set; }

    public int? ColumnIndex { get; set; }

    public double? Width { get; set; }

    public double? Height { get; set; }

    public virtual ICollection<PurchaseInvoiceItemsStorageSecor> PurchaseInvoiceItemsStorageSecors { get; set; } = new List<PurchaseInvoiceItemsStorageSecor>();

    public virtual Sector? Sector { get; set; }

    public virtual Storage? Storage { get; set; }
}
