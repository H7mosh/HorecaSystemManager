using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class BatchStorageSector
{
    public Guid Id { get; set; }

    public Guid? BatchId { get; set; }

    public Guid? StorageSectorId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Batch? Batch { get; set; }

    public virtual StorageSector? StorageSector { get; set; }
}
