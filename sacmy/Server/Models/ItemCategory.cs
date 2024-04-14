using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ItemCategory
{
    public Guid Id { get; set; }

    public Guid ItemId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid? SecondryCategoryId { get; set; }
}
