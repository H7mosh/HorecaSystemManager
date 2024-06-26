using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Brand
{
    public Guid Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? NameKr { get; set; }

    public string? NameTr { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>();
}
