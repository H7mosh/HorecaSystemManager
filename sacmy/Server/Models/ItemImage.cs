using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ItemImage
{
    public Guid Id { get; set; }

    public string? Link { get; set; }

    public Guid? ItemId { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public virtual Item1? Item { get; set; }
}
