using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Storage
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Branch { get; set; }

    public string? LocationDescription { get; set; }

    public string? Location { get; set; }

    public double? Width { get; set; }

    public double? Height { get; set; }

    public int? SectorsCount { get; set; }

    public virtual ICollection<StorageSector> StorageSectors { get; set; } = new List<StorageSector>();
}
