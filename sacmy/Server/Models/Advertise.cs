using System;
using System.Collections.Generic;

namespace sacmy.Server.Models;

public partial class Advertise
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Image { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
