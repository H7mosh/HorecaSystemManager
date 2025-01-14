using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class KpProductFirstImage
{
    public Guid ProductId { get; set; }

    public string? ImageLink { get; set; }
}
