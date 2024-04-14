using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ItemsItemSpecification
{
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public Guid SpecificationsId { get; set; }

    public virtual Item1 Item { get; set; } = null!;

    public virtual ItemSpecification Specifications { get; set; } = null!;
}
