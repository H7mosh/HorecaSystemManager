using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ItemSpecification
{
    public Guid Id { get; set; }

    public string? TitleTr { get; set; }

    public string? TitleEn { get; set; }

    public string? TitleAr { get; set; }

    public string? TitleKr { get; set; }

    public string? DescriptionEn { get; set; }

    public string? DescriptionTr { get; set; }

    public string? DescriptionAr { get; set; }

    public string? DescriptionKr { get; set; }

    public string? ImageName { get; set; }

    public virtual ICollection<ItemsItemSpecification> ItemsItemSpecifications { get; set; } = new List<ItemsItemSpecification>();
}
