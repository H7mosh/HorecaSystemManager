using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Sector
{
    public Guid Id { get; set; }

    public string? SectorDescriptionEn { get; set; }

    public string? SectorDescriptionAr { get; set; }

    public string? SectorDescriptionKr { get; set; }

    public int? Rows { get; set; }

    public int? Columns { get; set; }

    public virtual ICollection<StorageSector> StorageSectors { get; set; } = new List<StorageSector>();
}
