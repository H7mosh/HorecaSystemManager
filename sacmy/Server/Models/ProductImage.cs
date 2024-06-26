using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class ProductImage
{
    public Guid Id { get; set; }

    public string? Link { get; set; }

    public Guid? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
