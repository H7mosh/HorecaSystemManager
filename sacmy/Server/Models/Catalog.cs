using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Catalog
{
    public Guid Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? NameTr { get; set; }

    public string? NameKr { get; set; }

    public string? Image { get; set; }

    public Guid? BrandId { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual ICollection<SeriesOrCollectionOrCategory> SeriesOrCollectionOrCategories { get; set; } = new List<SeriesOrCollectionOrCategory>();
}
