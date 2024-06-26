using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class SeriesOrCollectionOrCategory
{
    public Guid Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? NameKr { get; set; }

    public string? NameTr { get; set; }

    public Guid? CatalogId { get; set; }

    public virtual Catalog? Catalog { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
